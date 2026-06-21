using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup hotbar;
    private InventorySlotButton[] hotbarImages;


    private void Start()
    {
        hotbarImages = hotbar.GetComponentsInChildren<InventorySlotButton>();
    }

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
            if (inventory[i].Id != "nullobj")
            {
                hotbarImages[i].SetSprite(inventory[i].ItemObject.Icon);
            }
            else
            {
                hotbarImages[i].SetSprite(null);
            }
        }
    }
}
