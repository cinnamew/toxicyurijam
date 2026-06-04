using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] private Inventory _playerInventory;
    private Item _selectedItem;

    private void Awake()
    {
        Instance = this;
        _selectedItem = null;
    }

    public bool AddItemToInventory(Item itemToAdd)
    {
        return _playerInventory.AddItem(itemToAdd);
    }

    public bool RemoveItemFromInventory(Item itemToRemove)
    {
        _selectedItem = null;
        return _playerInventory.RemoveByItemId(itemToRemove);
    }

    public bool RemoveItemFromInventory(ItemScriptableObject itemToRemove)
    {
        _selectedItem = null;
        return _playerInventory.RemoveByItemSOId(itemToRemove);
    }

    public void SetCurrentItem(int itemIndex)
    {
        if (itemIndex == -1)
        {
            _selectedItem = null;
            return;
        }
        Debug.Log("Setting item to : " + _playerInventory.GetItemAtIndex(itemIndex).ItemObject.Name);
        _selectedItem = _playerInventory.GetItemAtIndex(itemIndex);
    }

    public Item GetSelectedItem() => _selectedItem;
}
