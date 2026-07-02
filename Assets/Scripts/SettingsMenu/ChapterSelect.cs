using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour
{
    private const string NULLOBJ = "nullobj";

    private void Awake()
    {
        PlayerPrefs.HasKey(Globals.INVENTORY);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        // TODO: Change to MainMenu when it's ready
        if (scene.name == "ImportTesting") ResetInventory();
    }

    private void ResetInventory()
    {
        string[] inventory = new string[6]{NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ};
        PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, inventory));
    }

    public void SelectChapterInventory(int chapter)
    {
        string[] inventory = new string[6]{NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ, NULLOBJ};

        if (chapter == 0)
        {
            inventory[0] = "testing-item-star";
        }
        else if (chapter == 1)
        {
            inventory[1] = "testing-item-star";
        }
        else if (chapter == 2)
        {
            inventory[2] = "testing-item-star";
        }
        else if (chapter == 3)
        {
            inventory[3] = "testing-item-star";
        }
        else if (chapter == 4)
        {
            inventory[4] = "testing-item-star";
        }
        else if (chapter == 5)
        {
            inventory[5] = "testing-item-star";
        }
        else if (chapter == 6)
        {
            // add chapter 6 items
        }
        else if (chapter == 7)
        {
            // add chapter 7 items
        }

        PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, inventory));
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
