using System.Collections;
using Fungus;
using UnityEngine;

public class DialogueBoxUtilityHandler : MonoBehaviour, IHoverClickState
{
    [SerializeField] private DialogInput dialogInput;
    [SerializeField] private CanvasGroup dialogueBoxUtilityMenu;
    private UtilityMenuHandler settingsUtilityMenu;
    private bool isSkipping = false;
    
    private void Start()
    {
        settingsUtilityMenu = FindAnyObjectByType<UtilityMenuHandler>();
        if (settingsUtilityMenu == null) Debug.LogWarning("[DialogueBoxUtilityHandler]: No Utility Menu found in scene");
    }

    public void OpenLogs()
    {
        settingsUtilityMenu.OpenToTab(UtilityMenuHandler.UtilityTab.HISTORY);
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
        if (settingsUtilityMenu != null) settingsUtilityMenu.HideButtons();
        dialogueBoxUtilityMenu.alpha = 0;
        dialogueBoxUtilityMenu.interactable = false;
        dialogueBoxUtilityMenu.blocksRaycasts = false;
        DialogueClickStateManager.Instance.AddToList(this);
    }

    public void UnHide()
    {
        if (settingsUtilityMenu != null) settingsUtilityMenu.ShowButtons();
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
