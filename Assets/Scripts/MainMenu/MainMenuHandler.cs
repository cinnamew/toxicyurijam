using UnityEngine;
using UnityEngine.SceneManagement;

// reused for credits
public class MainMenuHandler : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsButtonPressed()
    {
        if (UtilityMenuHandler.Instance != null)
            UtilityMenuHandler.Instance.OpenToTab(0);
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
