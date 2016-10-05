using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Completed
{
    
    public class ItemsLoot : MonoBehaviour
    {

        public List<GameObject> listaItemsBDD;
        public List<GameObject> listaSlotsLootPanel;        
        public GameObject canvas_loot_Panel;
        public Canvas canvasUi;//para acomodar la vista en el drag and drop del nuevo item instanciado

        public void setUp()
        {            
            canvasUi = this.gameObject.GetComponent<Inventory>().canvas_interfaz_usuario.GetComponent<Canvas>();
            setUpLootPanel();
            setUplistaSlotsLootPanel();
        }

        public void setUpLootPanel()
        {
            canvas_loot_Panel = Instantiate(canvas_loot_Panel);
            canvas_loot_Panel.GetComponent<Canvas>().worldCamera = Camera.main; //setea la camara para ver bien la posicion del canvas
            canvas_loot_Panel.SetActive(false);            
        }

        private void setUplistaSlotsLootPanel()
        {
            Slot[] slots = canvas_loot_Panel.GetComponentsInChildren<Slot>();
            foreach (Slot slot in slots)
            {
                if (slot.tipo == "loot")
                {
                    listaSlotsLootPanel.Add(slot.gameObject);
                }

            }
        }

        
    }
}
