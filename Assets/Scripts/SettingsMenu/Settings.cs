using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Fungus;


public class Settings : MonoBehaviour
{
    private const int MAINMENU_SCENEID = 0;
    [SerializeField] private Slider writingSpeedSlider;
    [SerializeField] private TextMeshProUGUI successText;
    private Tween successTextColorTween = null;

    private void Start()
    {
        // DOTween.Init();
        writingSpeedSlider.onValueChanged.AddListener(val =>
        {
            Writer w = FindAnyObjectByType<Writer>();
            if (w != null) w.ChangeWritingSpeed(val > 60 ? 0 : val);
            else Debug.LogError("Writer not found");
            PlayerPrefs.SetFloat(Globals.WRITING_SPEED, val);
        });
    }

    public void OnSaveButtonPressed()
    {
        // Restart the tween when the button is spammed by setting the var back to null
        successTextColorTween?.Kill(true);
        successTextColorTween = null;

        // if (FindAnyObjectByType<LevelManager>() != null)
        // {
        //     LevelManager.Instance.SaveGame();
        //     successText.text = "Game Saved!";
        //     successText.color = Color.white;
        //     successTextColorTween = successText.DOColor(new Color(1, 1, 1, 0), 2f).SetDelay(2.5f).Play();
        // }
        // else
        // {
        //     successText.text = "Save Error!";
        //     successText.color = Color.red;
        //     successTextColorTween = successText.DOColor(new Color(1, 0, 0, 0), 2f).SetDelay(2.5f).Play();
        // }
    }

    public void OnTitleButtonPressed()
    {
        if (FindAnyObjectByType<SceneLoader>() == null) SceneManager.LoadScene(MAINMENU_SCENEID);
        // else SceneLoader.Instance.LoadSceneByBuildIndex(MAINMENU_SCENEID);
    }
}
