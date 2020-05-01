//This script was directly used from the 'HelloAR' example from the 'ARCore' SDK.
//The full script can be found in the project directory under 'GoogleARCore/Examples/HelloAR/Scripts/"
//Alternatively, the script can be found here: https://developers.google.com/ar/develop/unity/tutorials/hello-ar-sample
//Whilst the majority of this script remains the same, some functionality has been removed, such as spawning in items.
//This is because this functionality instead occurs in the main menu script.

//Using the default HelloAR example from the ARCore package installed locally onto the project.
namespace GoogleARCore.Examples.HelloAR
{
    using GoogleARCore;
    using UnityEngine;
    using UnityEngine.EventSystems;

    //Setting up the touch input propagation for Instant Preview within UNITY.
#if UNITY_EDITOR
    //ARCore feature.
    using Input = InstantPreviewInput;
#endif

    //Handles the HelloAR example.
    public class ARRendering : MonoBehaviour
    {
        //Stops doubles from spawning in.
        bool spawns = false;

        //Controls the AR camera used to render the camera image (such as the AR background).
        public Camera ARCamera;

        //Creation of a prefab variable to when a raycast from the user touch hits a detected plane (vertical).
        public GameObject DetectedPlanePrefab;

        //Prefab to place when a raycast from a users touch input hits a plane.
        public GameObject PrefabPlane;

        //Rotation in degrees needed to apply to a model when placed.
        private const float k_ModelRotation = 180.0f;

        //Returns true if the app is in the process of quitting due to an ARCore connection error, else return false.
        private bool m_IsQuitting = false;

        //Touch input setup.
        Touch touch;


        public void Awake()
        {
            //Enables ARCore to target 60gps camera capture frame rate on any supported device.
            Application.targetFrameRate = 60;
        }

        //Update method.
        public void Update()
        {
            _UpdateApplicationLifecycle();

            //If the player has not touched the screen, we are done with this update.
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began || spawns == true)
            {
                return;
            }
            //In the case that the player drags on screen, creates a paperball.

            //Should not handle inputs if the player is not pointing on the UI on-screen.
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                //Returns nothing.
                return;
            }

            //Raycast set against the location of the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                //Uses hit pose and camera pose to check if hittest is from the back of the plane, if so, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(ARCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    //Instantiate the ground in order to insert other objects within our scene.
                    Instantiate(PrefabPlane, hit.Pose.position, hit.Pose.rotation);
                    //Allow spawning in.
                    spawns = true;
                }
            }
        }
        //Fixed update method, if the spawns are true, change it to false to remove doubles.
        private void FixedUpdate()
        {
            if (spawns == true)
            {
                spawns = false;
            }
        }

        //Checking and updating an application's lifecycle.
        private void _UpdateApplicationLifecycle()
        {
            // Exit the app when the back button is pressed (for desktop testing only).
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            //Only allows screen to sleep when the device is not tracking.
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

            //Quits if ARCore is unable to connect and give Unity time for the toast message to appear.
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                //Pop-Up error message.
                _ShowAndroidToastMessage("Camera permission is required to run the application. Try again.");
                m_IsQuitting = true;

                //Quits app.
                Invoke("_DoQuit", 0.5f);
            }
            //If the status is an error in connection.
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting. Try connecting again");
                m_IsQuitting = true;

                //Quits app.
                Invoke("_DoQuit", 0.5f);
            }
        }

        //Quit the application.
        private void _DoQuit()
        {
            Application.Quit();
        }

        //Below is default toast messages that come with the HelloAR example.
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
