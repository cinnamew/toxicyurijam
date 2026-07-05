using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

// honestly script should probably be renamed to SliderSettings since it's not just the volume sliders
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider, sfxSlider, textSpeedSlider;
    private Writer currentWriter = null;

    private void Start()
    {
        FindWriterInScene();

        SetupVolumeSlider(musicSlider, Globals.MUSIC_VOLUME);
        SetupVolumeSlider(sfxSlider, Globals.SFX_VOLUME);

        textSpeedSlider.value = PlayerPrefs.GetFloat(Globals.WRITING_SPEED, 60f);
        textSpeedSlider.onValueChanged.AddListener(val =>
        {
            if (currentWriter != null) currentWriter.ChangeWritingSpeed(val >= 60 ? 0 : val);
            PlayerPrefs.SetFloat(Globals.WRITING_SPEED, val);
        });
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FindWriterInScene();
    }

    private void FindWriterInScene()
    {
        currentWriter = FindAnyObjectByType<Writer>();
        if (currentWriter != null) currentWriter.ChangeWritingSpeed(textSpeedSlider.value > 60 ? 0 : textSpeedSlider.value);
        else Debug.LogWarning("[VolumeSettings]: No dialogue box/writer found in scene: " + SceneManager.GetActiveScene().name);
    }

    private void SetupVolumeSlider(Slider slider, string globalReference)
    {
        slider.value = PlayerPrefs.GetFloat(globalReference, 1f);
        mainMixer.SetFloat(globalReference, 20 * Mathf.Log10(slider.value + float.Epsilon));

        slider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(globalReference, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(globalReference, slider.value);
        });
    }
}
