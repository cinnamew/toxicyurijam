using System.Collections;
using Fungus;
using UnityEngine;

public class DialogueBoxUtilityHandler : MonoBehaviour, IHoverClickState
{
    [SerializeField] private DialogInput dialogInput;
    [SerializeField] private CanvasGroup dialogueBoxUtilityMenu;

    public bool isSkipping { get; set; }

    private void Start()
    {
        if (UtilityMenuHandler.Instance == null) Debug.LogWarning("[DialogueBoxUtilityHandler]: No Utility Menu found in scene");
    }

    private void OnDisable()
    {
        isSkipping = false;
    }

    public void OpenLogs()
    {
        UtilityMenuHandler.Instance.OpenToTab(UtilityMenuHandler.UtilityTab.HISTORY);
    }

    public void Skip()
    {
        isSkipping = !isSkipping;
        if (isSkipping)
        {
            StartCoroutine(nameof(SkipText));
        }
    }

    public void Hide()
    {
        UtilityMenuHandler.Instance.HideButtons();
        dialogueBoxUtilityMenu.alpha = 0;
        dialogueBoxUtilityMenu.interactable = false;
        dialogueBoxUtilityMenu.blocksRaycasts = false;
        DialogueClickStateManager.Instance.AddToList(this);
    }

    public void UnHide()
    {
        UtilityMenuHandler.Instance.ShowButtons();
        dialogueBoxUtilityMenu.alpha = 1;
        dialogueBoxUtilityMenu.interactable = true;
        dialogueBoxUtilityMenu.blocksRaycasts = true;
        DialogueClickStateManager.Instance.RemoveFromList(this);
    }

    private IEnumerator SkipText()
    {
        while (isSkipping)
        {
            dialogInput.SetNextLineFlag();
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}
