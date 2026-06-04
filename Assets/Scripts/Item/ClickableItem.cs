using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableItem : MonoBehaviour, IPointerClickHandler
{
    [Header("General")]
    // Item ID for save/load system
    [SerializeField] protected string _id;

    // If you need the item to disappear after interacting with it
    [SerializeField] private bool _disappearOnInteract;

    // If the item is collectable, fill out the reference item ID from the database
    [Header("Inventory Management")]
    [SerializeField] private string _itemDatabaseId;
    private Item _item;

    // Add flowchart if interacting with item causes a flowchart block to execute
    [Header("Flowchart Management")]
    [SerializeField] private Flowchart _flowchart;
    [SerializeField] private string _blockToExecute;

    [Header("Debug")]
    [SerializeField] private bool _logDebugMessages;
    [SerializeField] private string _debugMessage;


    protected virtual void Start()
    {
        if (!string.IsNullOrWhiteSpace(_itemDatabaseId))
        {
            _item = new(ItemDatabase.GetItemById(_itemDatabaseId));
            _item.Id = _id;
        }
        // if (LevelManager.Instance.HasInteractedItem(_id) && _disappearOnInteract) gameObject.SetActive(false);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // LevelManager.Instance.AddToInteracetdItems(_id);
        if (_disappearOnInteract)
        {
            if (!string.IsNullOrWhiteSpace(_itemDatabaseId)) InventoryManager.Instance.AddItemToInventory(_item);
            if (GetComponent<Image>() != null)
            {
                GetComponent<Image>().raycastTarget = false;
                GetComponent<Image>().DOColor(new(1, 1, 1, 0), 0.25f).onComplete += () => gameObject.SetActive(false);
            }
            else
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().DOColor(new(1, 1, 1, 0), 0.25f).onComplete += () => gameObject.SetActive(false);
            }
            

        }
        if (_flowchart != null && !string.IsNullOrWhiteSpace(_blockToExecute)) _flowchart.ExecuteBlock(_blockToExecute);

        if (_logDebugMessages) Debug.Log(_debugMessage);
    }
}
