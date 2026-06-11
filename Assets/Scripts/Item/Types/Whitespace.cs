using UnityEngine;
using UnityEngine.EventSystems;

public class Whitespace : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click!");
        InventoryManager.Instance.SetCurrentItem(-1);
    }
}
