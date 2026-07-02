using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public string Id { get; set; } = "someIdforinventory";
    [SerializeField] private Item[] _items;
    private const int _capacity = 6;
    private string[] playerPrefsInventory;

    public static Action<Item[]> OnInventoryChanged;


    private void Awake()
    {
        _items = new Item[_capacity];
    }

    void Start()
    {
        if (PlayerPrefs.HasKey(Globals.INVENTORY))
        {
            playerPrefsInventory = PlayerPrefs.GetString(Globals.INVENTORY).Split(Globals.INV_SEPARATER);
        }
        else
        {
            playerPrefsInventory = new string[_capacity]{"nullobj", "nullobj", "nullobj", "nullobj", "nullobj", "nullobj"};
            PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
        }

        for (int i = 0; i < _capacity; i++)
        {
            _items[i] = new Item(ItemDatabase.GetItemById(playerPrefsInventory[i]));
            _items[i].Id = _items[i].ItemObject.Id;
        }

        OnInventoryChanged?.Invoke(_items);
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].ItemObject.Id == "nullobj")
            {
                _items[i] = itemToAdd;
                playerPrefsInventory[i] = itemToAdd.ItemObjectId;
                PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
                OnInventoryChanged?.Invoke(_items);
                return true;
            }
        }
        return false;
    }

    public bool RemoveByItemId(Item itemToRemove)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].Id == itemToRemove.Id)
            {
                _items[i] = new Item(ItemDatabase.GetItemById("nullobj"));
                playerPrefsInventory[i] = "nullobj";
                PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
                OnInventoryChanged?.Invoke(_items);
                return true;
            }
        }
        return false;
    }

    public bool RemoveByItemSOId(ItemScriptableObject itemToRemove)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].ItemObject.Id == itemToRemove.Id)
            {
                _items[i] = new Item(ItemDatabase.GetItemById("nullobj"));
                playerPrefsInventory[i] = "nullobj";
                PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
                OnInventoryChanged?.Invoke(_items);
                return true;
            }
        }
        return false;
    }

    public Item GetItemAtIndex(int index) => _items[index];
}
