using UnityEngine;
using UnityEngine.EventSystems;

class DisableClickOnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IHoverClickState
{
    [SerializeField] private bool showLogs;

    private void OnDisable()
    {
        if (FindAnyObjectByType<DialogueClickStateManager>() != null)
            DialogueClickStateManager.Instance.RemoveFromList(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogueClickStateManager.Instance.AddToList(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogueClickStateManager.Instance.RemoveFromList(this);
    }
}
