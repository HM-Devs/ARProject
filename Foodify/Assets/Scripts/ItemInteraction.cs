using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movementType
{
    none,movement, rotation, scale
}

public class ItemInteraction : MonoBehaviour
{
    // Start is called before the first frame update

    //these variables are used for translation, rotation and scale.
    MainMenu invT;
    //this is for the type of movement
    public movementType movType;
    //this is the position changed by the user when draging on the object
    Vector3 dragPosition;

    void Start()
    {
        invT = GameObject.FindGameObjectWithTag("inventory").GetComponent<MainMenu>(); ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }


    public void checkRaycast()
    {
        //create a raycast and check if it is hitting the ground
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "ground")
            {
                dragPosition = hit.point;
            }
        }
    }

    
    public void OnMouseUp()
    {
       gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void OnMouseDrag()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;

        if (movType==movementType.movement)
        {
            checkRaycast();
            move(dragPosition);
        }

        else if (movType == movementType.rotation)
        {
            checkRaycast();
            rotate(transform.position-dragPosition);
        }

        else if (movType == movementType.scale)
        {
            checkRaycast();
            scale((transform.position - dragPosition).magnitude/2);
        }
        
    }

    public void move(Vector3 pos)
    {
        transform.position = pos;
    }
    public void rotate(Vector3 dir)
    {
        transform.right = dir;
        transform.rotation*= Quaternion.Euler(-90, 0, 0); ;
    }
    public void scale(float mult)
    {
        transform.localScale = mult*new Vector3(1,1,1);
    }
}
