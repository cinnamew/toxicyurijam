using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Fungus;
using UnityEngine;

public class DialogueClickStateManager : PersistentSingleton<DialogueClickStateManager>
{
    private List<IHoverClickState> disabledList;
    private DialogInput dialogInput;

    protected override void Awake()
    {
        base.Awake();
        disabledList = new List<IHoverClickState>();
    }

    private void Start()
    {
        dialogInput = FindAnyObjectByType<DialogInput>();
        disabledList.Clear();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetDisabled;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetDisabled;
    }

    private void ResetDisabled(Scene scene, LoadSceneMode loadSceneMode)
    {
        Start();
    }

    public void RemoveFromList(IHoverClickState objectToRemove)
    {
        disabledList.Remove(objectToRemove);
        if (disabledList.Count < 1)
        {
            dialogInput.clickMode = ClickMode.ClickAnywhere;
        }
        Debug.Log("Removing");
    }

    public void AddToList(IHoverClickState objectToAdd)
    {
        disabledList.Add(objectToAdd);
        dialogInput.clickMode = ClickMode.Disabled;
        Debug.Log("Adding");
    }
}

public interface IHoverClickState {  }
