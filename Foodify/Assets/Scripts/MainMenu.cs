using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //this variable knows which type has the user selected
    public typeOfFurniture selectedFurniture;

    // this is used to know which gameobject was selected by the user
    public GameObject inventoryObject;
    public int selectedIndex;

    //these are the gameobjects refered to the lights
    [Header("FURNITURE GAMEOBJECTS ")]
    public GameObject[] lights;
    public GameObject[] chair_tables;
    public GameObject[] self;
    public GameObject[] beds;
    public GameObject[] decoration;
    public GameObject[] toilet;

    [Header("FURNITURE ICONS ")]
    public Sprite[] lights_icons;
    public Sprite[] chair_tables_icons;
    public Sprite[] self_icons;
    public Sprite[] beds_icons;
    public Sprite[] decoration_icons;
    public Sprite[] toilet_icons;

    [Header("OTHER VARIABLES")]
    //this is the scroll view content
    public Transform scrollView;
    //this is the prefab used to generate the icons
    public GameObject iconImage;
    // this is the text on top of the container with the title
    public Text inventoryText;
    // this is the rectangle tansform of the scrollview
    RectTransform rectTransf;
    //the animation of the panel
    Animator anim;

    //the selected gameobject
    public GameObject selectedObject=null;

    void Start()
    {
        rectTransf = scrollView.GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {

            /////////////////////
            // TYPE TEST
            /////////////////////
            //check the selected object
            if(selectedFurniture==typeOfFurniture.lights)
            {
                inventoryObject = lights[selectedIndex];
            }
            //check the selected object
            else if (selectedFurniture == typeOfFurniture.tables_chairs)
            {
                inventoryObject = chair_tables[selectedIndex];
            }
            //check the selected object
            else if (selectedFurniture == typeOfFurniture.self)
            {
                inventoryObject = self[selectedIndex];
            }
            //check the selected object
            else if (selectedFurniture == typeOfFurniture.beds)
            {
                inventoryObject = beds[selectedIndex];
            }
            //check the selected object
            else if (selectedFurniture == typeOfFurniture.decoration)
            {
                inventoryObject = decoration[selectedIndex];
            }
            //check the selected object
            else if (selectedFurniture == typeOfFurniture.toilet)
            {
                inventoryObject = toilet[selectedIndex];
            }
   


            ////////////////////////////
            // RAYCAST HIT TEST
            ///////////////////////////
            //we use a raycast to know where the user has clicked
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "ground" )
                {
                    if (selectedObject == null)
                    {
                        //this is the instance of the inventory object
                        GameObject go = Instantiate(inventoryObject, hit.point, Quaternion.Euler(-90, 0, 0));

                        //set scale
                        go.transform.localScale = go.transform.localScale*0.8f;
                        //get and set components
                        go.AddComponent<ItemInteraction>();
                        //go.AddComponent<BoxCollider>();
                        go.tag = "object";
                    }else
                    {
                        
                        ItemInteraction objcIntS = selectedObject.GetComponent<ItemInteraction>();
                        objcIntS.movType = movementType.none;

                        selectedObject = null;
                    }
                }
                else if(hit.collider.gameObject.tag == "object")
                {
                    selectedObject = hit.collider.gameObject;

                }
            }

        }



    }

    //it is called when pressing the light button
    public void generateLightMenu()
    {
        openInventory();
        clearMenu();
        inventoryText.text = "Lights";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (lights.Length/2 + 2f) * 150);


        for (int i = 0; i < lights.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage,scrollView);
            RectTransform rectT =goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS =goI.GetComponent<ItemThumbnail>();
            

            int div = i / 2;
            int rest = i % 2;


            if (rest==0)
            {
                goI.transform.localPosition =new Vector3((1-0.5f)*rectT.sizeDelta.x, -(div+1.1f)* rectT.sizeDelta.y,0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2-0.5f)*rectT.sizeDelta.x, -(div+1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = lights_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.lights;
            iconS.indx = i;

           
        }
    }
    //it is called when pressing the chair/tables button
    public void generateChairTableMenu()
    {
        openInventory();
        clearMenu();
        inventoryText.text = "Chairs and Tables";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (chair_tables.Length / 2 + 2f) * 150);

        for (int i = 0; i < chair_tables.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage, scrollView);
            RectTransform rectT = goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS = goI.GetComponent<ItemThumbnail>();

            int div = i / 2;
            int rest = i % 2;


            if (rest == 0)
            {
                goI.transform.localPosition = new Vector3((1 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = chair_tables_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.tables_chairs;
            iconS.indx = i;

          
        }
    }
    //it is called when pressing the self button
    public void generateSelfMenu()
    {
        clearMenu();
        openInventory();

        inventoryText.text = "Shelves";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (self.Length / 2 + 2f) * 150);

        for (int i = 0; i < self.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage, scrollView);
            RectTransform rectT = goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS = goI.GetComponent<ItemThumbnail>();

            //this is used to determine the position of the image for odds and pair values
            int div = i / 2;
            int rest = i % 2;


            if (rest == 0)
            {
                goI.transform.localPosition = new Vector3((1 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = self_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.self;
            iconS.indx = i;
            
       }
    }
    public void generateBedsMenu()
    {
        clearMenu();
        openInventory();
        inventoryText.text = "Beds";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (beds.Length / 2 + 2f) * 150);

        for (int i = 0; i < beds.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage, scrollView);
            RectTransform rectT = goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS = goI.GetComponent<ItemThumbnail>();

            //this is used to determine the position of the image for odds and pair values
            int div = i / 2;
            int rest = i % 2;


            if (rest == 0)
            {
                goI.transform.localPosition = new Vector3((1 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = beds_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.beds;
            iconS.indx = i;
            
       }
    }
    public void generateDecorationMenu()
    {
        clearMenu();
        openInventory();
        inventoryText.text = "Decoration";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (decoration.Length / 2 + 2f) * 150);

        for (int i = 0; i < decoration.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage, scrollView);
            RectTransform rectT = goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS = goI.GetComponent<ItemThumbnail>();

            //this is used to determine the position of the image for odds and pair values
            int div = i / 2;
            int rest = i % 2;


            if (rest == 0)
            {
                goI.transform.localPosition = new Vector3((1 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = decoration_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.decoration;
            iconS.indx = i;

        }
    }
    public void generateToiletMenu()
    {
        clearMenu();
        openInventory();
        inventoryText.text = "Bathroom";

        //set the rectangle transform to the correct size for the content
        rectTransf.sizeDelta = new Vector2(rectTransf.sizeDelta.x, (toilet.Length / 2 + 2f) * 150);

        for (int i = 0; i < toilet.Length; i++)
        {
            //get the gameocomponents inside the icon
            GameObject goI = Instantiate(iconImage, scrollView);
            RectTransform rectT = goI.GetComponent<RectTransform>();
            Image imgR = goI.GetComponent<Image>();
            ItemThumbnail iconS = goI.GetComponent<ItemThumbnail>();

            //this is used to determine the position of the image for odds and pair values
            int div = i / 2;
            int rest = i % 2;


            if (rest == 0)
            {
                goI.transform.localPosition = new Vector3((1 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0);
            }
            else
            {
                goI.transform.localPosition = new Vector3((2 - 0.5f) * rectT.sizeDelta.x, -(div + 1.1f) * rectT.sizeDelta.y, 0); ;
            }

            //set the sprite
            imgR.sprite = toilet_icons[i];

            //set the icon scripts
            iconS.type = typeOfFurniture.toilet;
            iconS.indx = i;

        }
    }

    //destroys the gameobjects in the scene inside the menu
    public void clearMenu()
    {
        //get all the gameobjects and destroy them
        GameObject[] go = GameObject.FindGameObjectsWithTag("icon");

        for(int i=0;i<go.Length;i++)
        {
            Destroy(go[i]);
        }
    }

    public void openInventory()
    {
        anim.SetBool("open", true);
        
    }
    public void closeInventory()
    {
        anim.SetBool("open", false);
    }


    public void setMovement()
    {
        if(selectedObject!=null)
        {
            //get the interaction script and set the movement
            ItemInteraction objcIntS = selectedObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.movement;
        }
    }

    public void setRotation()
    {
        if (selectedObject != null)
        {
            //get the interaction script and set the movement
            ItemInteraction objcIntS = selectedObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.rotation;
        }
    }

    public void setScale()
    {
        if (selectedObject != null)
        {
            //get the interaction script and set the movement
            ItemInteraction objcIntS = selectedObject.GetComponent<ItemInteraction>();
            objcIntS.movType = movementType.scale;
        }
    }

    public void delete()
    {
        if (selectedObject != null)
        {
            //get the interaction script and set the movement
            Destroy(selectedObject);
        }
    }

}
