//This script is used to instantiate and control the instantiation of item thumbnails.
//This script was adapted from the following existing unity asset: https://assetstore.unity.com/packages/tools/gui/ar-gallery-inventory-room-153316 
//This script was used to help understand the best way to implement item icon images to use within the created menu system and provided an understanding to
//Concepts such as Script inheritance, using unity tags, index selection from an array. 

using UnityEngine;

//Setting up the type of menu sub category users can choose from.
//Similar to how item interaction functions
public enum typeOfMenuItem
{
    starters,mains,desserts,drinks,sides
}

//ItemThumbnail class which handles the thumbnail image of every item.
public class ItemThumbnail : MonoBehaviour
{
    //Variable setup for item index, item type
    public MainMenu menuS;
    public typeOfMenuItem type;
    public int index = 0;

    //Start method is called before the first frame updates.
    void Start()
    {
        //Our main menu is set to find gameobjects with the tag 'inventory' to begin with.
        menuS = GameObject.FindGameObjectWithTag("inventory").GetComponent<MainMenu>();
    }

    //call to the menu to set the gameobject
    public void selectItemFromMenu()
    {
        //Checks if item menu type is of starters.
        if(type==typeOfMenuItem.starters)
        {
            //Sets the selected item from subcategories.
            menuS.selectedMenuItem = typeOfMenuItem.starters;
            
        }
        //Checks if item menu type is of mains.
        else if (type == typeOfMenuItem.mains)
        {
            //Sets the selected item from subcategories.
            menuS.selectedMenuItem = typeOfMenuItem.mains;
            
        }
        //Checks if item menu type is of desserts.
        else if (type == typeOfMenuItem.desserts)
        {
            //Sets the selected item from subcategories.
            menuS.selectedMenuItem = typeOfMenuItem.desserts;

        }
        //Checks if item menu type is of drinks.
        else if (type == typeOfMenuItem.drinks)
        {
            //Sets the selected item from subcategories.
            menuS.selectedMenuItem = typeOfMenuItem.drinks;

        }
        //Checks if item menu type is of sides.
        else if (type == typeOfMenuItem.sides)
        {
            //Sets the selected item from subcategories.
            menuS.selectedMenuItem = typeOfMenuItem.sides;

        }
        //the selected index location from the main menu becomes the new index.
        menuS.selectedItemIndex = index;
    }

}
