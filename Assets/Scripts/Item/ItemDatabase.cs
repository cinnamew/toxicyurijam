using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    // In this string, all the items for the database will be located at "./Resources/Items/"
    private const string ITEM_LOCATION = "Items";
    public static Dictionary<string, ItemScriptableObject> ItemDictionary;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void Initialize()
    {
        ItemDictionary = new Dictionary<string, ItemScriptableObject>();

        ItemScriptableObject[] itemList = Resources.LoadAll<ItemScriptableObject>(ITEM_LOCATION);
        foreach (ItemScriptableObject item in itemList)
        {
            ItemDictionary.Add(item.Id, item);
        }
    }

    public static ItemScriptableObject GetItemById(string id)
    {
        try
        {
            return ItemDictionary[id];
        }
        catch
        {
            Debug.LogError($"[ItemDatabase]: Cannot find item with id {id}");
            return null;
        }
    }
}
