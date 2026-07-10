using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// reused for credits
public class MainMenuHandler : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChapterSelectButtonPressed()
    {
        if (UtilityMenuHandler.Instance != null)
            UtilityMenuHandler.Instance.OpenToTab(1);
    }

    public void SettingsButtonPressed()
    {
        if (UtilityMenuHandler.Instance != null)
            UtilityMenuHandler.Instance.OpenToTab(0);
    }

    public void ExitGameButtonPressed()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("Who gave you permission to leave?");
#else
        Application.Quit();
#endif
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
