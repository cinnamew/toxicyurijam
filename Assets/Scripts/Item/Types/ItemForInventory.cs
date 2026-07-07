using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemForInventory : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("Unique ID for this item.")]
    private string id;

    [SerializeField, Tooltip("The item ID as it is listed in the Item Resources folder.")] 
    private string itemDatabaseReferenceId;

    [SerializeField, Tooltip("Should the item disappear when it is clicked?")] 
    private bool disappearOnInteract;
    
    private Item item;


    private void Start()
    {
        if (FindFirstObjectByType<InventoryManager>() == null) 
            Debug.LogError("[ItemForInventory]: Inventory Manager not in scene.");

        item = new(ItemDatabase.GetItemById(itemDatabaseReferenceId))
        {
            Id = id
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrWhiteSpace(itemDatabaseReferenceId)) 
            InventoryManager.Instance.AddItemToInventory(item);
            
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().raycastTarget = false;
            GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.25f).onComplete += () => gameObject.SetActive(false);
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().DOColor(new(1, 1, 1, 0), 0.25f).onComplete += () => gameObject.SetActive(false);
        }
    }
}
