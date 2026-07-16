using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueBoxUtilityButtonHandler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    [SerializeField] private UnityEvent onSelect;
    [SerializeField] private UnityEvent onDeselect;
    private bool isSelected = false;

    public void OnSelect(BaseEventData eventData)
    {
        onSelect.Invoke();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        onDeselect.Invoke();
        isSelected = false;
    }

    private void OnDisable()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
            isSelected = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject && isSelected)
        {
            EventSystem.current.SetSelectedGameObject(null);
            isSelected = false;
        }
        else
        {
            isSelected = true;
        }
    }
}
