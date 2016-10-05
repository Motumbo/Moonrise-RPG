using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Completed
{


public class SelectedItem : MonoBehaviour 
{
    
    private Item item;
    public Text description;
    public Text stats;
    private Slot lastSlot;

    	
    public void RefreshData(Item item)
    {
        if (lastSlot != null)
        {
            lastSlot.transform.GetComponent<Image>().color = new Color(255F, 255F, 255F, 100F);
        }
        
        item.transform.parent.gameObject.GetComponent<Image>().color = Color.yellow;
        lastSlot = item.transform.parent.GetComponent<Slot>();
        description.text = item.description;
    }
}
}