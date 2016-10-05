using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Completed
{


public class Item : MonoBehaviour 
{
    public string name;
    public string description;
    public int level;
    public string slotTipe;
    public string inventoryType;
    public string rarity;
    public float strength;
    public float agility;
    public float wisdom;
    public float maxlife;
    public float maxmana;
    public float liferegen;
    public float manaregen;
    public float blockchance;
    public float stunchance;
    public float criticalstrikechance;
    public float piercechance;
    public float attackspeed;
    public float dodgechance;
    public float immunitychance;
    public float movementspeed;
    public float damage;
    public float armor;

    void OnMouseDown()
    {
         Inventory.instance.pickUpItem(this.gameObject);      
    }
    public void showTooltip()
        
    {
        if (DragItemHandler.ItemBeingDragged == null)
        {
            Inventory.instance.showTooltip(this.gameObject);
        }
        
    }
    public void hideTooltip()
    {
        Inventory.instance.hideTooltip();
    }
   
    
}
}