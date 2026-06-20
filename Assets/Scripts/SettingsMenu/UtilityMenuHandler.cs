using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityMenuHandler : PersistentSingleton<UtilityMenuHandler>
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

    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private CanvasGroup chapterPanel;
    [SerializeField] private CanvasGroup historyPanel;
    private CanvasGroup[] tabs;
    private UtilityTab currentTab;


    private void Start()
    {
        tabs = new CanvasGroup[3]{settingsPanel, chapterPanel, historyPanel};
    }

    public void ChangeTab(int tab)
    {
        if (tab > tabs.Length) tab = tabs.Length - 1;

        tabs[tab].alpha = 0;
        tabs[tab].interactable = false;
        tabs[tab].blocksRaycasts = false;

        tabs[(int)currentTab].alpha = 1;
        tabs[(int)currentTab].interactable = true;
        tabs[(int)currentTab].blocksRaycasts = true;

        currentTab = (UtilityTab)tab;
    }

    public void OpenSettings()
    {
        settingsMenu.alpha = 1;
        settingsMenu.interactable = true;
        settingsMenu.blocksRaycasts = true;
    }

    public void CloseSettings()
    {
        settingsMenu.alpha = 0;
        settingsMenu.interactable = false;
        settingsMenu.blocksRaycasts = false;
    }













    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == MAINMENU_SCENEID || scene.buildIndex == MUSICON_SCENEID)
        {
            buttonContainer.SetActive(false);
            CloseSettingsPanel();
        }
        else
        {
            buttonContainer.SetActive(true);
        }
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.alpha = 1;
        settingsPanel.interactable = true;
        settingsPanel.blocksRaycasts = true;
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.alpha = 0;
        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
    }
}
