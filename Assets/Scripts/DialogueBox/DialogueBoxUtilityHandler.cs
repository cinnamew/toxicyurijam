using System.Collections;
using Fungus;
using UnityEngine;

public class DialogueBoxUtilityHandler : MonoBehaviour, IHoverClickState
{
    [SerializeField] private DialogInput dialogInput;
    [SerializeField] private Writer writer;
    [SerializeField] private CanvasGroup dialogueBoxUtilityMenu;

    private float autoSpeed = 1.0f;
    private bool isSkipping = false;
    private bool isAuto = false;


    private void Start()
    {
        if (UtilityMenuHandler.Instance == null) Debug.LogWarning("[DialogueBoxUtilityHandler]: No Utility Menu found in scene");
    }

    private void OnDisable()
    {
        isSkipping = false;
        isAuto = false;
    }

    public void OpenLogs()
    {
        UtilityMenuHandler.Instance.OpenToTab(UtilityMenuHandler.UtilityTab.HISTORY);
    }

    public void SkipOn()
    {
        isSkipping = !isSkipping;
        if (isSkipping)
        {
            StartCoroutine(nameof(SkipText));
        }
    }

    public void SkipOff()
    {
        isSkipping = false;
    }

    public void AutoOn()
    {
        if (isSkipping)
        {
            isSkipping = false;
            StopCoroutine(nameof(SkipText));
        }

        isAuto = true;
        if (isAuto)
        {
            StartCoroutine(nameof(AutoText));
        }
    }

    public void AutoOff()
    {
        isAuto = false;
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

    private IEnumerator AutoText()
    {
        while (isAuto)
        {
            if (writer.FinishedLine)
            {
                writer.FinishedLine = false;
                yield return new WaitForSeconds(autoSpeed);
                dialogInput.SetNextLineFlag();
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    public void SetAutoSpeed(float newSpeed) => autoSpeed = newSpeed;
}
