using UnityEngine;
using UnityEngine.SceneManagement;

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
}
