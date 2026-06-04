using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    void OnEnable()
    {
        Inventory.OnInventoryChanged += UpdateInventoryDisplay;
    }

    void OnDisable()
    {
        Inventory.OnInventoryChanged -= UpdateInventoryDisplay;
    }

    private void UpdateInventoryDisplay(Item[] inventory)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            Debug.Log(inventory[i].Id);
            if (inventory[i].Id != "nullobj")
            {
                SetSprite(transform.GetChild(i).GetChild(0).GetComponent<Image>(), inventory[i].ItemObject.Icon);
            }
            else
            {
                SetSprite(transform.GetChild(i).GetChild(0).GetComponent<Image>(), null);
            }
        }
    }

    private void SetSprite(Image imgToSet, Sprite sprToSet)
    {
        imgToSet.sprite = sprToSet;
        imgToSet.color = sprToSet == null ? Color.clear : Color.white;
        imgToSet.preserveAspect = true;
    }
}
