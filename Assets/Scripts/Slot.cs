using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Completed
{


public class Slot : MonoBehaviour , IDropHandler
{
    public string tipo;//tipoi de slot debe ser igual al tipo de item
    private Transform newParent;   
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)//si tiene algun hijo, seria el item 
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
        
    }

    public bool isEmpty()
    {
        return this.transform.childCount == 0;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!Inventory.instance.itemsLoot.canvas_loot_Panel.gameObject.activeInHierarchy)
        {

            if (!item)//si el slot no tiene un item
            {
                if ((DragItemHandler.ItemBeingDragged.GetComponent<Item>().inventoryType == this.tipo) || (this.tipo == "") || (DragItemHandler.ItemBeingDragged.GetComponent<Item>().slotTipe == this.tipo))//si el slot es del inventario o no especificado o si el slot es del mismo tipo del item que estoy arrastrando
                {
                    DragItemHandler.ItemBeingDragged.transform.SetParent(transform);
                }

            }
            else
            {
                newParent = DragItemHandler.ItemBeingDragged.transform.parent;//guardo el padre del item que estoy arrastrandp
                if ((((this.tipo == "inventario") || (this.tipo == "") || (DragItemHandler.ItemBeingDragged.GetComponent<Item>().slotTipe == this.tipo))) && (((item.GetComponent<Item>().slotTipe == newParent.GetComponent<Slot>().tipo) || (newParent.GetComponent<Slot>().tipo == "") || (newParent.GetComponent<Slot>().tipo == "inventario"))))// y si el item q voy a reemplazar es compatble con el slot del cual comence a arrastrar
                {
                    DragItemHandler.ItemBeingDragged.transform.SetParent(transform);//el nuevo padre de dicho item es el slot donde lo dejo
                    item.transform.SetParent(newParent);     //el padre q habia guardado del item es el nuevo padre del item al cual reemplace 
                }

            }
            Inventory.instance.updateDataOnPanel();
        }
    }
}
}