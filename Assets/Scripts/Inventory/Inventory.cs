using System;
using UnityEngine;

public class Inventory : MonoBehaviour /*IBind<InventoryData>*/
{
    [field: SerializeField] public string Id { get; set; } = "someIdforinventory";
    [SerializeField] private Item[] _items;
    // private InventoryData _inventoryData = new();
    private readonly int _capacity = 6;

    public static Action<Item[]> OnInventoryChanged;


    private void Awake()
    {
        _items = new Item[_capacity];
        for (int i = 0; i < _capacity; i++)
        {
            _items[i] = new Item(ItemDatabase.GetItemById("nullobj"));
            _items[i].Id = _items[i].ItemObject.Id;
        }
    }

    void Start()
    {
        OnInventoryChanged?.Invoke(_items);
    }

    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i].ItemObject.Id == "nullobj")
            {
                _items[i] = itemToAdd;
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
                OnInventoryChanged?.Invoke(_items);
                return true;
            }
        }
        return false;
    }

    public Item GetItemAtIndex(int index) => _items[index];

    // public void Bind(InventoryData data)
    // {
    //     _inventoryData = data;

    //     bool isNew = _inventoryData.Items == null || _inventoryData.Items.Length == 0;

    //     if (isNew) _inventoryData.Items = new Item[_capacity];
    //     else
    //     {
    //         for (int i = 0; i < _capacity; i++)
    //         {
    //             _inventoryData.Items[i].ItemObject = ItemDatabase.GetItemById(_inventoryData.Items[i].ItemObjectId);
    //         }
    //     }

    //     if (isNew && _items.Length != 0)
    //     {
    //         for (int i = 0; i < _capacity; i++)
    //         {
    //             _inventoryData.Items[i] = _items[i];
    //         }
    //     }

    //     _items = _inventoryData.Items;
    //     data.Id = Id;
    //     OnInventoryChanged?.Invoke(_items);
    // }
}


