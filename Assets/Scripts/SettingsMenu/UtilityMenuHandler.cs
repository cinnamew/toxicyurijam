using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityMenuHandler : PersistentSingleton<UtilityMenuHandler>, IHoverClickState
{
    public enum UtilityTab
    {
        SETTINGS = 0,
        CHAPTER_SELECT,
        HISTORY
    };


    // Scenes to hide utility menu
    private const int MAINMENU_SCENEID = 0;
    private const int MUSICON_SCENEID = 1;

    [SerializeField] private Animator animator;
    private int isOpenHash;

    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private CanvasGroup chapterPanel;
    [SerializeField] private CanvasGroup historyPanel;
    [SerializeField] private GameObject blocker;
    [SerializeField] private CanvasGroup narrativeLog;
    private CanvasGroup[] tabs;
    private UtilityTab currentTab;


    private void Start()
    {
        tabs = new CanvasGroup[3]{settingsPanel, chapterPanel, historyPanel};
        isOpenHash = Animator.StringToHash("IsOpen");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeTab(int tab)
    {
        if (tab > tabs.Length) tab = tabs.Length - 1;

        if (tab != (int)currentTab)
        {
            tabs[(int)currentTab].alpha = 0;
            tabs[(int)currentTab].interactable = false;
            tabs[(int)currentTab].blocksRaycasts = false;
        }

        tabs[tab].alpha = 1;
        tabs[tab].interactable = true;
        tabs[tab].blocksRaycasts = true;

        currentTab = (UtilityTab)tab;
    }

    public void OpenToTab(int tab)
    {
        ChangeTab(tab);
        animator.SetBool(isOpenHash, true);
        narrativeLog.alpha = 1;
        if (DialogueClickStateManager.instance != null) DialogueClickStateManager.instance.AddToList(this);
    }

    public void OpenToTab(UtilityTab tab) => OpenToTab((int)tab);

    public void CloseSettings()
    {
        animator.SetBool(isOpenHash, false);
        if (DialogueClickStateManager.instance != null) DialogueClickStateManager.instance.RemoveFromList(this);
    }

    public void HideButtons()
    {
        buttonContainer.SetActive(false);
    }

    public void ShowButtons()
    {
        buttonContainer.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == MAINMENU_SCENEID)
        {
            HideButtons();
        }
        else
        {
            ShowButtons();
        }
    }

    public void ReturnToMainMenu()
    {
        CloseSettings();
        SceneManager.LoadScene(MAINMENU_SCENEID);
    }

    // public void OpenSettingsPanel()
    // {
    //     settingsPanel.alpha = 1;
    //     settingsPanel.interactable = true;
    //     settingsPanel.blocksRaycasts = true;
    // }

    // public void CloseSettingsPanel()
    // {
    //     settingsPanel.alpha = 0;
    //     settingsPanel.interactable = false;
    //     settingsPanel.blocksRaycasts = false;
    // }
}
