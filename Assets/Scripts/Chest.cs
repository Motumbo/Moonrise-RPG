using UnityEngine;
using System.Collections;
namespace Completed
{
    public class Chest : MonoBehaviour
    {

        public string rarity;
        public string description;
        public string estado;//cerrado , abierto

        /*public int cantidadItems()//rarity + player lvl
        {
            int cantidad = GameManager.instance.getDifficultylvl() / 10;

            if (cantidad <= 0)
            {
                cantidad = 1;
            }
            if (rarity == "Rare")
            {
                cantidad++;
            }
            if (rarity == "Unique")
            {
                cantidad = cantidad + 2;
            }
            if (rarity == "Legendary")
            {
                cantidad = cantidad + 3;
            }
            return cantidad;
        }
        public void openChest()
        {            
            if (estado == "Cerrado")
            {
                ItemsLoot il = Inventory.instance.GetComponent<ItemsLoot>();
                for (int i = 0; i < cantidadItems(); i++)
                {
                    GameObject item = Instantiate(il.listaItemsBDD[Random.Range(1, il.listaItemsBDD.Count)]);
                    item.GetComponent<ItemRandom>().generateItemStats();
                    item.GetComponent<DragItemHandler>().Canvas = il.canvasUi;
                    item.transform.SetParent(il.listaSlotsLootPanel[i].transform);
                    item.transform.localScale = new Vector3(1, 1, 1);

                }
                estado = "Abierto";
            }
            
        }

        */
    }
}