using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string Id = "null";
    public string Name;
    public Sprite Icon;
    [TextArea()] public string Description;
}
