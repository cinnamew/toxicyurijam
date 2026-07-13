using UnityEngine;
using Fungus;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockToExecute;
    [SerializeField] private UnityEvent onClick;

    private void Start()
    {
        if (flowchart == null) 
            Debug.LogError("[DialogueItem]: No flowchart set.");
        if (string.IsNullOrWhiteSpace(blockToExecute))
            Debug.LogWarning("[DialogueItem]: Block to execute not set.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (flowchart != null && !string.IsNullOrWhiteSpace(blockToExecute))
        {
            flowchart.ExecuteBlock(blockToExecute);
            onClick?.Invoke();
        } 
    }
}
