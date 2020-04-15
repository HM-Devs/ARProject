using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Note:Everything set up here is essentially controlled within the MainMenuInteractions gameobject
// in the MenuPrefab folder.

//Creation of the MainMenu class.
public class MainMenu : MonoBehaviour
{
    //Variable used to know what type of menu item has been selected from a list of selected menu items.
    public typeOfMenuItem selectedMenuItem;

    //Gameobject to know what game object has been selected by user, from its item index.
    public GameObject menuObject;
    public int selectedItemIndex;

    //Gameobject creation for each sub-menu category. 
    [Header("Main Menu Categories ")]

    public GameObject[] starters;
    public GameObject[] mains;
    public GameObject[] desserts;
    public GameObject[] drinks;
    public GameObject[] sides;


    //Thumbnail sprites attached to each menu sub-category - used to show users images of the item they're about to order.
    [Header("Menu Category Thumbnails ")]

    public Sprite[] starters_icons;
    public Sprite[] mains_icons;
    public Sprite[] desserts_icons;
    public Sprite[] drinks_icons;
    public Sprite[] sides_icons;


    //Other UI settings setup here which are configured within the MainMenuInteractions gameobject.
    [Header("Menu UI Settings")]

    //Setup of the scroll view for our menu.
    public Transform menuScrollView;

    //Created prefab used to generate thumbnail images of each menu item used.
    public GameObject thumbnailImage;

    //Text of each sub-menu category - e.g (Starter, Mains, Drinks...)
    public Text mainMenuText;

    //GameObject used to open and close our menu
    public GameObject menuSettings;

    //Rectangular transform of the instantiated scrollview.
    RectTransform rectTransform;

    //Selected gameobject from the menu(default set to null until an actual item is selected by the user)
    public GameObject selectedMenuObject = null;

    //Start method, happens as soon as the app loads.
    void Start()
    {
        //We create our rect transform for our menu.
        rectTransform = menuScrollView.GetComponent<RectTransform>();

        //By default, the menu is set to closed (false) - as it would get in the way of users trying the app.
        menuSettings.SetActive(false);
    }

    //The update method called every frame.
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Checks the selected menu item, based on its type (starters)
            if (selectedMenuItem == typeOfMenuItem.starters)
            {
                menuObject = starters[selectedItemIndex];
            }
            //Checks the selected menu item, based on its type (mains)
            else if (selectedMenuItem == typeOfMenuItem.mains)
            {
                menuObject = mains[selectedItemIndex];
            }
            //Checks the selected menu item, based on its type (desserts)
            else if (selectedMenuItem == typeOfMenuItem.desserts)
            {
                menuObject = desserts[selectedItemIndex];
            }
            //Checks the selected menu item, based on its type (drinks)
            else if (selectedMenuItem == typeOfMenuItem.drinks)
            {
                menuObject = drinks[selectedItemIndex];
            }
            //Checks the selected menu item, based on its type (sides)
            else if (selectedMenuItem == typeOfMenuItem.sides)
            {
                menuObject = sides[selectedItemIndex];
            }

            //A raycast is setup to track where users have clicked.
            RaycastHit hits;

            //Tracks based on mouse/touch input.
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

            //If a raycast is is used and a hit is made.
            if (Physics.Raycast(raycast, out hits))
            {
                //Nested loop to see if the gameobject collided with has the tag ground (which is the plane).
                if (hits.collider.gameObject.tag == "ground")
                {
                    //If the item selected is currently null.
                    if (selectedMenuObject == null)
                    {
                        //We instantiate the gameobject item.
                        GameObject go = Instantiate(menuObject, hits.point, Quaternion.Euler(-90, 0, 0));

                        //We also set the scale.
                        go.transform.localScale = go.transform.localScale * 0.8f;

                        //And get and set components.
                        go.AddComponent<ItemInteraction>();
                        go.tag = "object";
                    }
                    else
                    {
                        //Otherwise we set the movement type to none and set the game object to null since none of the
                        //Pre-conditions in our initial if-statement have been made.
                        ItemInteraction objcIntS = selectedMenuObject.GetComponent<ItemInteraction>();
                        objcIntS.movType = movementType.none;

                        //Object selected is now null.
                        selectedMenuObject = null;
                    }
                }
                //If the game object collided with has the tag object
                else if (hits.collider.gameObject.tag == "object")
                {
                    //Then the collision with the raycast is made.
                    selectedMenuObject = hits.collider.gameObject;

                }
            }

        }
    }

    //The starters menu is created when selected from the main menu.
    public void createStartersMenu()
    {
        //Inventory is opened and the menu is preset to clear.
        openInventory();
        clearMenu();

        //The text of the sub-category is displayed at the top of the menu.
        mainMenuText.text = "Starters";

        //Set the rectangle transform to fit a correct size for the required content to load.
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
            (starters.Length / 2 + 2f) * 150);

        //If statement for starters
        for (int i = 0; i < starters.Length; i++)
        {
            //Retrieve the game-components inside of the thumbnail image used, including:
            //The actual gameobject thumbnail, the rect transform element, the image used and the script attached (ItemThumbnail.cs)
            GameObject gameobjectThumbnail = Instantiate(thumbnailImage, menuScrollView);

            RectTransform rectTransform = gameobjectThumbnail.GetComponent<RectTransform>();

            Image imgThumbnail = gameobjectThumbnail.GetComponent<Image>();

            ItemThumbnail thumbnailScript = gameobjectThumbnail.GetComponent<ItemThumbnail>();

            //Sizing the thumbnail directly.
            int div = i / 2;
            int rest = i % 2;

            //If its set to 0 (even)
            if (rest == 0)
            {
                //Transform the thumbnail image as shown
                gameobjectThumbnail.transform.localPosition = new Vector3((1 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0);
            }
            else
            {
                //Format the thumbnail image like this instead
                gameobjectThumbnail.transform.localPosition = new Vector3((2 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0); ;
            }

            //Finds the relevant thumbnail sprite element from the starters icon list.
            imgThumbnail.sprite = starters_icons[i];

            //Sets our thumbnail script for every icon used.
            thumbnailScript.type = typeOfMenuItem.starters;
            thumbnailScript.indx = i;


        }
    }
    //it is called when pressing the chair/tables button
    public void createMainsMenu()
    {
        //Inventory is opened and the menu is preset to clear.
        openInventory();
        clearMenu();

        //The text of the sub-category is displayed at the top of the menu.
        mainMenuText.text = "Mains";

        //Set the rectangle transform to fit a correct size for the required content to load.
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
            (mains.Length / 2 + 2f) * 150);

        //If statement for starters
        for (int i = 0; i < mains.Length; i++)
        {
            //Retrieve the game-components inside of the thumbnail image used, including:
            //The actual gameobject thumbnail, the rect transform element, the image used and the script attached (ItemThumbnail.cs)
            GameObject gameobjectThumbnail = Instantiate(thumbnailImage, menuScrollView);

            RectTransform rectTransform = gameobjectThumbnail.GetComponent<RectTransform>();

            Image imgThumbnail = gameobjectThumbnail.GetComponent<Image>();

            ItemThumbnail thumbnailScript = gameobjectThumbnail.GetComponent<ItemThumbnail>();

            //Sizing the thumbnail directly.
            int div = i / 2;
            int rest = i % 2;

            //If its set to 0 (even)
            if (rest == 0)
            {
                //Transform the thumbnail image as shown
                gameobjectThumbnail.transform.localPosition = new Vector3((1 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0);
            }
            else
            {
                //Format the thumbnail image like this instead
                gameobjectThumbnail.transform.localPosition = new Vector3((2 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0); ;
            }

            //Finds the relevant thumbnail sprite element from the starters icon list.
            imgThumbnail.sprite = mains_icons[i];

            //Sets our thumbnail script for every icon used.
            thumbnailScript.type = typeOfMenuItem.mains;
            thumbnailScript.indx = i;


        }
    }
    //it is called when pressing the self button
    public void createDessertsMenu()
    {
        //Inventory is opened and the menu is preset to clear.
        openInventory();
        clearMenu();

        //The text of the sub-category is displayed at the top of the menu.
        mainMenuText.text = "Desserts";

        //Set the rectangle transform to fit a correct size for the required content to load.
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
            (desserts.Length / 2 + 2f) * 150);

        //If statement for starters
        for (int i = 0; i < desserts.Length; i++)
        {
            //Retrieve the game-components inside of the thumbnail image used, including:
            //The actual gameobject thumbnail, the rect transform element, the image used and the script attached (ItemThumbnail.cs)
            GameObject gameobjectThumbnail = Instantiate(thumbnailImage, menuScrollView);

            RectTransform rectTransform = gameobjectThumbnail.GetComponent<RectTransform>();

            Image imgThumbnail = gameobjectThumbnail.GetComponent<Image>();

            ItemThumbnail thumbnailScript = gameobjectThumbnail.GetComponent<ItemThumbnail>();

            //Sizing the thumbnail directly.
            int div = i / 2;
            int rest = i % 2;

            //If its set to 0 (even)
            if (rest == 0)
            {
                //Transform the thumbnail image as shown
                gameobjectThumbnail.transform.localPosition = new Vector3((1 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0);
            }
            else
            {
                //Format the thumbnail image like this instead
                gameobjectThumbnail.transform.localPosition = new Vector3((2 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0); ;
            }

            //Finds the relevant thumbnail sprite element from the starters icon list.
            imgThumbnail.sprite = desserts_icons[i];

            //Sets our thumbnail script for every icon used.
            thumbnailScript.type = typeOfMenuItem.desserts;
            thumbnailScript.indx = i;


        }
    }
    public void createDrinksMenu()
    {
        //Inventory is opened and the menu is preset to clear.
        openInventory();
        clearMenu();

        //The text of the sub-category is displayed at the top of the menu.
        mainMenuText.text = "Drinks";

        //Set the rectangle transform to fit a correct size for the required content to load.
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
            (drinks.Length / 2 + 2f) * 150);

        //If statement for starters
        for (int i = 0; i < drinks.Length; i++)
        {
            //Retrieve the game-components inside of the thumbnail image used, including:
            //The actual gameobject thumbnail, the rect transform element, the image used and the script attached (ItemThumbnail.cs)
            GameObject gameobjectThumbnail = Instantiate(thumbnailImage, menuScrollView);

            RectTransform rectTransform = gameobjectThumbnail.GetComponent<RectTransform>();

            Image imgThumbnail = gameobjectThumbnail.GetComponent<Image>();

            ItemThumbnail thumbnailScript = gameobjectThumbnail.GetComponent<ItemThumbnail>();

            //Sizing the thumbnail directly.
            int div = i / 2;
            int rest = i % 2;

            //If its set to 0 (even)
            if (rest == 0)
            {
                //Transform the thumbnail image as shown
                gameobjectThumbnail.transform.localPosition = new Vector3((1 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0);
            }
            else
            {
                //Format the thumbnail image like this instead
                gameobjectThumbnail.transform.localPosition = new Vector3((2 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0); ;
            }

            //Finds the relevant thumbnail sprite element from the starters icon list.
            imgThumbnail.sprite = drinks_icons[i];

            //Sets our thumbnail script for every icon used.
            thumbnailScript.type = typeOfMenuItem.drinks;
            thumbnailScript.indx = i;
        }
    }

    public void createSidesMenu()
    {
        //Inventory is opened and the menu is preset to clear.
        openInventory();
        clearMenu();

        //The text of the sub-category is displayed at the top of the menu.
        mainMenuText.text = "Sides";

        //Set the rectangle transform to fit a correct size for the required content to load.
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x,
            (sides.Length / 2 + 2f) * 150);

        //If statement for starters
        for (int i = 0; i < sides.Length; i++)
        {
            //Retrieve the game-components inside of the thumbnail image used, including:
            //The actual gameobject thumbnail, the rect transform element, the image used and the script attached (ItemThumbnail.cs)
            GameObject gameobjectThumbnail = Instantiate(thumbnailImage, menuScrollView);

            RectTransform rectTransform = gameobjectThumbnail.GetComponent<RectTransform>();

            Image imgThumbnail = gameobjectThumbnail.GetComponent<Image>();

            ItemThumbnail thumbnailScript = gameobjectThumbnail.GetComponent<ItemThumbnail>();

            //Sizing the thumbnail directly.
            int div = i / 2;
            int rest = i % 2;

            //If its set to 0 (even)
            if (rest == 0)
            {
                //Transform the thumbnail image as shown
                gameobjectThumbnail.transform.localPosition = new Vector3((1 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0);
            }
            else
            {
                //Format the thumbnail image like this instead
                gameobjectThumbnail.transform.localPosition = new Vector3((2 - 0.5f) * rectTransform.sizeDelta.x, -(div + 1.1f) * rectTransform.sizeDelta.y, 0); ;
            }

            //Finds the relevant thumbnail sprite element from the starters icon list.
            imgThumbnail.sprite = sides_icons[i];

            //Sets our thumbnail script for every icon used.
            thumbnailScript.type = typeOfMenuItem.sides;
            thumbnailScript.indx = i;
        }
    }

    //Causes the removal of gameobjects within the menu.
    public void clearMenu()
    {
        //Find all instances of gameobjects and remove them all.
        GameObject[] destroy = GameObject.FindGameObjectsWithTag("icon");

        //For loop to find all elements of the gameobject with tag "icon"
        for (int i = 0; i < destroy.Length; i++)
        {
            //Destroy these game objects within all index locations of point i
            Destroy(destroy[i]);
        }
    }

    //Function to open the menu.
    public void openInventory()
    {
        //Set the gameobject instantiated at the top of class (menuSettings) to active-true, causing the menu to open.
        menuSettings.SetActive(true);
    }

    //Function to close the menu, default set to this function to not get in the way of user screen.
    public void closeInventory()
    {
        //Set the gameobject instantiated at the top of class (menuSettings) to active-false, causing the menu to close.
        menuSettings.SetActive(false);
    }

    //Controls the menu object's movement.
    public void setMovement()
    {
        //If the game object is selected and exists...(not null)
        if (selectedMenuObject != null)
        {
            //Get the item interaction script and set the movement of the given item.
            ItemInteraction objcIntS = selectedMenuObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.movement;
        }
    }

    //Controls the menu object's rotation.
    public void setRotation()
    {
        //If the game object is selected and exists...(not null)
        if (selectedMenuObject != null)
        {
            //Get the item interaction script and set the rotation of the given item.
            ItemInteraction objcIntS = selectedMenuObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.rotation;
        }
    }

    //Controls the menu object's scaling.
    public void setScale()
    {
        //If the game object is selected and exists...(not null)
        if (selectedMenuObject != null)
        {
            //Get the item interaction script and set the scaling of the item.
            ItemInteraction objcIntS = selectedMenuObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.scale;
        }
    }

    //Deletes the game object selected from the menu.
    public void delete()
    {
        //If the game object is selected and exists...(not null)s
        if (selectedMenuObject != null)
        {
            //Destroy the selected game object.
            Destroy(selectedMenuObject);
        }
    }

}
