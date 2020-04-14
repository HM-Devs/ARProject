using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum typeOfFurniture
{
    lights,tables_chairs,self,beds,decoration,toilet
}

public class ItemThumbnail : MonoBehaviour
{
    public MainMenu inv;
    public typeOfFurniture type;
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
        if(type==typeOfFurniture.lights)
        {
            inv.selectedFurniture = typeOfFurniture.lights;
            
        }
        else if (type == typeOfFurniture.tables_chairs)
        {
            inv.selectedFurniture = typeOfFurniture.tables_chairs;
            
        }
        else if (type == typeOfFurniture.self)
        {
            inv.selectedFurniture = typeOfFurniture.self;

        }
        else if (type == typeOfFurniture.beds)
        {
            inv.selectedFurniture = typeOfFurniture.beds;

        }
        else if (type == typeOfFurniture.decoration)
        {
            inv.selectedFurniture = typeOfFurniture.decoration;

        }
        else if (type == typeOfFurniture.toilet)
        {
            inv.selectedFurniture = typeOfFurniture.toilet;

        }
        inv.selectedIndex = indx;
    }

}
