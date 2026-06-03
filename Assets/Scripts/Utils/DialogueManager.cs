using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class DialogueManager : PersistentSingleton<DialogueManager>
{
    [SerializeField] private Writer writer;
    [SerializeField] private Flowchart flowchart;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu") PlayerPrefs.SetString("currScene", SceneManager.GetActiveScene().name);
        writer = FindAnyObjectByType<Writer>();
    }

    public void ChangeWritingSpeed(float f)
    {
        if (writer != null)
            if (f > 60) writer.ChangeWritingSpeed(0);
            else writer.ChangeWritingSpeed(f);
        else
            Debug.LogError("Writer not found");
    }

    public void RunBlock(string blockName)
    {
        flowchart.ExecuteBlock(blockName);
    }

    public void ChangeDialogInputClickMode(ClickMode clickMode)
    {
        // DialogInput[] dialogInputs = GameObject.FindObjectsOfType(typeof(DialogInput)) as DialogInput[];
        DialogInput[] dialogInputs = FindObjectsByType<DialogInput>(FindObjectsSortMode.None);
        foreach (DialogInput dialogInput in dialogInputs)
        {
            dialogInput.clickMode = clickMode;
        }
    }
}
