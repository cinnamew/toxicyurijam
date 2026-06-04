using UnityEngine;

[System.Serializable]
public class Item
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string ItemObjectId { get; set; }
    public ItemScriptableObject ItemObject;

    public Item(ItemScriptableObject itemObject)
    {
        ItemObject = itemObject;
        ItemObjectId = itemObject.Id;
    }
}
