using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.EventSystems;

public class LockedClickableItem : MonoBehaviour, IPointerClickHandler
{
    [Header("General")]
    [SerializeField] private string _id;
    [SerializeField] private ItemScriptableObject _keyToOpen;
    [SerializeField] private bool _disappearOnInteract;
    [SerializeField] private float _disappearTweenValue;
    [SerializeField] private GameObject[] _revealObjects;
    private bool _isOpen = false;
    private const int NULL_ITEM = -1;

    [Header("Fungus Flowchart")]
    [SerializeField] private Flowchart _flowchart;
    [SerializeField] private string _inspectBlock;
    [SerializeField] private string _failToOpenBlock;
    [SerializeField] private string _openedBlock;

    [Header("Debug")]
    [SerializeField] private bool _logDebugMessages;
    private const string INSPECT_DEBUG_MSG = "[LockedClickableItem]: Inspecting...";
    private const string OPEN_DEBUG_MSG = "[LockedClickableItem]: Opened.";
    private const string FAIL_DEBUG_MSG = "[LockedClickableItem]: Wrong item used.";


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isOpen) return;
        if (InventoryManager.Instance.GetSelectedItem() == null || InventoryManager.Instance.GetSelectedItem().Id == "nullobj")
        {
            if (_logDebugMessages) Debug.Log(INSPECT_DEBUG_MSG);
            if (TryGetBlock(_inspectBlock)) _flowchart.ExecuteBlock(_inspectBlock);
            InventoryManager.Instance.SetCurrentItem(NULL_ITEM);
            return;
        }

        if (InventoryManager.Instance.GetSelectedItem().ItemObjectId == _keyToOpen.Id)
        {
            if (_logDebugMessages) Debug.Log(OPEN_DEBUG_MSG);
            InventoryManager.Instance.RemoveItemFromInventory(InventoryManager.Instance.GetSelectedItem());
            Open();
            if (TryGetBlock(_openedBlock)) _flowchart.ExecuteBlock(_openedBlock);
        }
        else
        {
            if (_logDebugMessages) Debug.Log(FAIL_DEBUG_MSG);
            if (TryGetBlock(_failToOpenBlock)) _flowchart.ExecuteBlock(_failToOpenBlock);
        }
        InventoryManager.Instance.SetCurrentItem(NULL_ITEM);
    }

    private void Open()
    {
        _isOpen = true;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (GameObject g in _revealObjects)
        {
            g.SetActive(true);
        }
        if (_disappearOnInteract) GetComponent<SpriteRenderer>().DOColor(new(1, 1, 1, 0), _disappearTweenValue).onComplete += () => gameObject.SetActive(false);
    }

    private bool TryGetBlock(string blockName) => _flowchart != null && _flowchart.HasBlock(blockName);
}
