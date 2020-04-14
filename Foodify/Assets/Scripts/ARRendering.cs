
//namespace
namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

   
    /// Controls the HelloAR example.
    
    public class ARRendering : MonoBehaviour
    {
       
        //prevent doubles
        bool spawn = false;


        /// The first-person camera being used to render the passthrough camera image (i.e. AR
        /// background).

        public Camera FirstPersonCamera;

       
        /// A prefab for tracking and visualizing detected planes.
        
        public GameObject DetectedPlanePrefab;

       
        /// A model to place when a raycast from a user touch hits a plane.
        
        public GameObject PrefabPlane;

       
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        
        private const float k_ModelRotation = 180.0f;

       
        /// True if the app is in the process of quitting due to an ARCore connection error,
        /// otherwise false.
        
        private bool m_IsQuitting = false;

        Touch touch;

        /// The Unity Update() method.

        private void Start()
        {
           
        }


        public void Update()
        {
            _UpdateApplicationLifecycle();
                       


            // If the player has not touched the screen, we are done with this update.
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began || spawn==true)
            {
                return;
            }
            // in the case that the player drags on screen, create a paperball


           


            // Should not handle input if the player is pointing on UI.
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    // Choose the Andy model for the Trackable that got hit.
                    /* GameObject prefab;
                     if (hit.Trackable is FeaturePoint)
                     {
                         prefab = PrefabMarker;
                     }
                     else
                     {
                         prefab = PrefabMarker;
                     }*/

                     // Create an anchor to allow ARCore to track the hitpoint as understanding of
                        // the physical world evolves.

                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);


                    /////////////////////////////////
                    // GROUND
                    ///////////////////////////////////
                    //isntantiate GROUND in order to insert the other objects in the scene

                    Instantiate(PrefabPlane, hit.Pose.position, hit.Pose.rotation);

                    spawn = true;
                 

                }
            }
        }


        private void FixedUpdate()
        {
            if(spawn==true)
            {
                spawn = false;
            }
        }


        /// Check and update the application lifecycle.

        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the 'back' button is pressed.
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            // Only allow the screen to sleep when not tracking.
            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            // Quit if ARCore was unable to connect and give Unity some time for the toast to
            // appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

       
        /// Actually quit the application.
        
        private void _DoQuit()
        {
            Application.Quit();
        }

       
        /// Show an Android toast message.
        
        /// <param name="message">Message string to show in the toast.</param>
        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
