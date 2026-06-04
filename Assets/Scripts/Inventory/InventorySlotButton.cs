using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotButton : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        InventoryManager.Instance.SetCurrentItem(transform.GetSiblingIndex());
    }
}
