using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilityMenuHandler : PersistentSingleton<UtilityMenuHandler>
{
    // Scenes to hide utility menu
    private const int MAINMENU_SCENEID = 0;
    private const int MUSICON_SCENEID = 1;

    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private CanvasGroup settingsPanel;

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
