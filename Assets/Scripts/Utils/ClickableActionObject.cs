using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableActionObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke();
    }
}
