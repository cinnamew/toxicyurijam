using UnityEngine;
using UnityEngine.EventSystems;
using Fungus;

public class IgnoreDialogueClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool disabled = false;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (DialogueManager.Instance != null) DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.Disabled);
    }

    public void ChangeClick()
    {
        if (!disabled)
        {
            DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.Disabled);
            disabled = true;
        }
        else
        {
            disabled = false;
            DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
        }
    }

    public void DisableClick()
    {
        DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.Disabled);
        disabled = true;
    }

    public void EnableClick()
    {
        DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
        disabled = false;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!disabled && DialogueManager.Instance != null) DialogueManager.Instance.ChangeDialogInputClickMode(ClickMode.ClickAnywhere);
    }
}
