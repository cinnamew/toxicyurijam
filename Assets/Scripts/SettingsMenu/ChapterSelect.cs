using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterSelect : MonoBehaviour
{
    private const string NULLOBJ = "nullobj";
    private const int INV_CAPACITY = 7;
    [SerializeField] private Transform chapterButtonContainer;
    private const int ENDINGS_COUNT = 3;
    private int chaptersSeen;
    private int endingsSeen;  // Uses bitwise


    private void Awake()
    {
        chaptersSeen = PlayerPrefs.GetInt(Globals.SCENE_SEEN, 0);
        endingsSeen = PlayerPrefs.GetInt(Globals.ENDING_SEEN, 0);
        // chaptersSeen = 15;  // developer debug
    }

    private void Start()
    {
        UpdateChapterLocks();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        if (scene.name == "MainMenu") ResetInventory();

        if (scene.name == "1_Day 1")
        {
            if (chaptersSeen < 1)
            {
                chaptersSeen = 1;
            }
        }
        else if (scene.name == "3_Day 2")
        {
            if (chaptersSeen < 2)
            {
                chaptersSeen = 2;
            }
        }
        else if (scene.name == "5_Day 3")
        {
            if (chaptersSeen < 3)
            {
                chaptersSeen = 3;
            }
        }
        else if (scene.name == "6_Day 4")
        {
            if (chaptersSeen < 4)
            {
                chaptersSeen = 4;
            }
        }
        else if (scene.name == "6_Day 4 Post Minigame")
        {
            if (chaptersSeen < 5)
            {
                chaptersSeen = 5;
            }
        }
        else if (scene.name == "7_Day 6")
        {
            if (chaptersSeen < 6)
            {
                chaptersSeen = 6;
            }
        }
        else if (scene.name == "9_Day 7")
        {
            if (chaptersSeen < 7)
            {
                chaptersSeen = 7;
            }
        }
        else if (scene.name == "Ending 1")
        {
            endingsSeen |= 1 << 0;
        }
        else if (scene.name == "Ending 2")
        {
            endingsSeen |= 1 << 1;
        }
        else if (scene.name == "Ending 3")
        {
            endingsSeen |= 1 << 2;
        }

        PlayerPrefs.SetInt(Globals.SCENE_SEEN, chaptersSeen);
        PlayerPrefs.SetInt(Globals.ENDING_SEEN, endingsSeen);
    }

    private void ResetInventory()
    {
        string[] inventory = new string[INV_CAPACITY];
        Array.Fill(inventory, NULLOBJ);
        PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, inventory));
    }

    public void SelectChapterInventory(int chapter)
    {
        string[] inventory = new string[INV_CAPACITY];
        Array.Fill(inventory, NULLOBJ);

        switch (chapter)
        {
            case 0:
            case 1:
                break;
            case 2:
                inventory[0] = "book";
                break;
            case 3:
                inventory[1] = "hemlock";
                inventory[2] = "lily";
                inventory[3] = "wort";
                goto case 2;
            case 7:
                inventory[4] = "lavender";
                inventory[5] = "bezoar";
                goto case 3;
            default:
                goto case 3;
        }

        PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, inventory));
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void UpdateChapterLocks()
    {
        // Checks chapters
        for (int i = 0; i < chapterButtonContainer.childCount - ENDINGS_COUNT; i++)
        {
            Transform chapterButtonObject = chapterButtonContainer.GetChild(i);
            Button chapterButton = chapterButtonObject.GetComponent<Button>();
            GameObject lockedImage = chapterButtonObject.GetChild(1).gameObject;
            GameObject unlockedImage = chapterButtonObject.GetChild(0).gameObject;
            if (i <= chaptersSeen)
            {
                chapterButton.interactable = true;
                unlockedImage.SetActive(true);
                lockedImage.SetActive(false);
            }
            else
            {
                chapterButton.interactable = false;
                unlockedImage.SetActive(false);
                lockedImage.SetActive(true);
            }
        }

        // Checks endings
        int offset = chapterButtonContainer.childCount - ENDINGS_COUNT;
        for (int i = offset; i < chapterButtonContainer.childCount; i++)
        {
            Transform chapterButtonObject = chapterButtonContainer.GetChild(i);
            Button chapterButton = chapterButtonObject.GetComponent<Button>();
            GameObject lockedImage = chapterButtonObject.GetChild(1).gameObject;
            GameObject unlockedImage = chapterButtonObject.GetChild(0).gameObject;
            if ((endingsSeen & (1 << (i - offset))) != 0)
            {
                chapterButton.interactable = true;
                unlockedImage.SetActive(true);
                lockedImage.SetActive(false);
            }
            else
            {
                chapterButton.interactable = false;
                unlockedImage.SetActive(false);
                lockedImage.SetActive(true);
            }
        }
    }
}
