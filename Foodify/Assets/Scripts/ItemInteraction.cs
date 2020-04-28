using UnityEngine;

//Movement types for the item
public enum itemMovementType
{
    //Selection for the type of movement based action to apply to each menu item -  none, movement, rotation or scaling.
    none, movement, rotation, scale
}

//ItemInteraction class.
public class ItemInteraction : MonoBehaviour
{
    // Start is called before the first frame update, with the variables below used for translation, rotation and scaling.-
    MainMenu menuS;

    //Used for item (from menu) movement.
    public itemMovementType movementSetting;
    
    //Position changed by user when they drag an object on-screen.
    Vector3 dragItemPosition;

    //On game start, find objects with the tag inventory from the mainmenu.cs script.
    void Start()
    {
        menuS = GameObject.FindGameObjectWithTag("inventory").GetComponent<MainMenu>(); ;
    }

    //Creates a raycast to check if its hitting the ground.
    public void checkRaycast()
    {
        RaycastHit hits;
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(raycast, out hits))
        {
            if (hits.collider.gameObject.tag == "ground")
            {
                //Sets item position to the point the raycast has hit.
                dragItemPosition = hits.point;
            }
        }
    }

    //When touch/mouse is used upwards
    public void OnMouseUp()
    {
        //Ensured the box-collider element is enabled.
       gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    //On touch/mouse drag
    public void OnMouseDrag()
    {
        //Ensure the box collision element is no longer element when the user drags an element.
        gameObject.GetComponent<BoxCollider>().enabled = false;

        //If statement: if movement is selected
        if (movementSetting==itemMovementType.movement)
        {
            //Checks the current raycast
            checkRaycast();
            //Move an item using drag position.
            menuItemMove(dragItemPosition);
        }

        //Else if the rotation button is pressed
        else if (movementSetting == itemMovementType.rotation)
        {
            //Checks the current raycast
            checkRaycast();

            //Moves an item using a set-rotation based on the users position-their drag position.
            menuItemRotate(transform.position-dragItemPosition);
        }

        //Else if the scale button is pressed
        else if (movementSetting == itemMovementType.scale)
        {
            //Checks the current raycast.
            checkRaycast();
            
            //Scales an item based on its current position and its drag position and magnitude/2.
            menuItemScale((transform.position - dragItemPosition).magnitude/2);
        } 
    }

    //Move function for the attached button.
    public void menuItemMove(Vector3 pos)
    {
        transform.position = pos;
    }
    
    //Rotate function for the attached button.
    public void menuItemRotate(Vector3 dir)
    {
        transform.right = dir;
        transform.rotation*= Quaternion.Euler(-90, 0, 0); ;
    }

    //Scale button for the attached button.
    public void menuItemScale(float mult)
    {
        transform.localScale = mult*new Vector3(1,1,1);
    }
}
