using UnityEngine;
using UnityEngine.EventSystems;

class DisableClickOnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IHoverClickState
{
    [SerializeField] private bool showLogs;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogueClickStateManager.Instance.AddToList(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogueClickStateManager.Instance.RemoveFromList(this);
    }
}
