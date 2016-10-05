using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Completed
{
    public class DragItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        public static GameObject ItemBeingDragged;

        public Canvas Canvas;

        Vector3 startPosition;
        Transform startParent;
        private Image image;
        public GameObject slot;
        ItemsLoot il;
        private Vector3 screenPoint;
        private Vector3 offset;


        public void OnBeginDrag(PointerEventData eventData)
        {
            il = Inventory.instance.GetComponent<ItemsLoot>();
            Canvas = il.canvasUi;
            if (!il.canvas_loot_Panel.gameObject.activeInHierarchy)
            {
                Inventory.instance.selectedItem = this.gameObject;
                Inventory.instance.updateDataOnPanel();
                slot = gameObject.transform.parent.gameObject;
                image = slot.GetComponent<Image>();
                image.color = Color.green;

                ItemBeingDragged = gameObject;

                startPosition = transform.position;
                startParent = transform.parent;
                GetComponent<CanvasGroup>().blocksRaycasts = false;

                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }


        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!il.canvas_loot_Panel.gameObject.activeInHierarchy)
            {   /*             
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas.transform as RectTransform,
                Input.mousePosition, Canvas.worldCamera, out pos);
                transform.position = Canvas.transform.TransformPoint(pos);
                 DEPRECATED*/
                    
                float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!il.canvas_loot_Panel.gameObject.activeInHierarchy)
            {
                image.color = new Color(255F, 255F, 255F, 0.25F);

                ItemBeingDragged = null;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                if (transform.parent == startParent)//si no es un slot vuelve al inicio
                {
                    transform.position = startPosition;
                }
            }
        }

    }
}