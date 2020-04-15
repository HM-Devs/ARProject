using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum typeOfMenuItem
{
    starters,mains,desserts,drinks,sides
}

public class ItemThumbnail : MonoBehaviour
{
    public MainMenu inv;
    public typeOfMenuItem type;
    public int indx = 0;
    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("inventory").GetComponent<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //call to the inventory to set the gameobject
    public void selectGameObject_fromInventory()
    {
        if(type==typeOfMenuItem.starters)
        {
            inv.selectedMenuItem = typeOfMenuItem.starters;
            
        }
        else if (type == typeOfMenuItem.mains)
        {
            inv.selectedMenuItem = typeOfMenuItem.mains;
            
        }
        else if (type == typeOfMenuItem.desserts)
        {
            inv.selectedMenuItem = typeOfMenuItem.desserts;

        }
        else if (type == typeOfMenuItem.drinks)
        {
            inv.selectedMenuItem = typeOfMenuItem.drinks;

        }
        else if (type == typeOfMenuItem.sides)
        {
            inv.selectedMenuItem = typeOfMenuItem.sides;

        }
        inv.selectedItemIndex = indx;
    }

}
