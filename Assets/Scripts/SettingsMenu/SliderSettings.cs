using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class SliderSettings : MonoBehaviour
{
    // Constants
    private const float DEFAULT_VOLUME = 1.0f;
    private const float DEFAULT_WRITING_SPEED = 60.0f;
    private const float INSTANT_WRITING_SPEED = 0.0f;
    private const float DEFAULT_AUTO_SPEED = 1.0f;

    // Editor Fields
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider, sfxSlider, textSpeedSlider, autoSpeedSlider;

    // Dependency References & Private Fields
    private Writer currentWriter = null;
    private DialogueBoxUtilityHandler currentDialogueBoxUtilities = null;
    private float maxWritingSpeed;

    // Debug
    [Header("Debug")]
    [SerializeField] private bool logMessages;


    private void Start()
    {
        maxWritingSpeed = textSpeedSlider.maxValue;

        SetupVolumeSlider(musicSlider, Globals.MUSIC_VOLUME);
        SetupVolumeSlider(sfxSlider, Globals.SFX_VOLUME);
        textSpeedSlider.value = PlayerPrefs.GetFloat(Globals.WRITING_SPEED, DEFAULT_WRITING_SPEED);
        autoSpeedSlider.value = PlayerPrefs.GetFloat(Globals.AUTO_SPEED, DEFAULT_AUTO_SPEED);
        if (currentDialogueBoxUtilities != null) currentDialogueBoxUtilities.SetAutoSpeed(autoSpeedSlider.value);

        textSpeedSlider.onValueChanged.AddListener(val =>
        {
            if (currentWriter != null) currentWriter.ChangeWritingSpeed(val >= maxWritingSpeed ? INSTANT_WRITING_SPEED : val);
            PlayerPrefs.SetFloat(Globals.WRITING_SPEED, val);
        });
        autoSpeedSlider.onValueChanged.AddListener(val =>
        {
            if (currentDialogueBoxUtilities != null) currentDialogueBoxUtilities.SetAutoSpeed(val);
            PlayerPrefs.SetFloat(Globals.AUTO_SPEED, val);
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
        FindDialogueBoxUtilitiesInScene();
    }

    private void FindWriterInScene()
    {
        currentWriter = FindAnyObjectByType<Writer>();
        if (currentWriter != null) currentWriter.ChangeWritingSpeed(textSpeedSlider.value >= maxWritingSpeed ? INSTANT_WRITING_SPEED : textSpeedSlider.value);
        else if (logMessages) Debug.LogWarning("[VolumeSettings]: No dialogue box/writer found in scene: " + SceneManager.GetActiveScene().name);
    }

    private void FindDialogueBoxUtilitiesInScene()
    {
        currentDialogueBoxUtilities = FindAnyObjectByType<DialogueBoxUtilityHandler>();
        if (currentDialogueBoxUtilities != null) currentDialogueBoxUtilities.SetAutoSpeed(autoSpeedSlider.value);
        else if (logMessages) Debug.LogWarning("[VolumeSettings]: No dialogue box utilites found in scene: " + SceneManager.GetActiveScene().name);
    }

    private void SetupVolumeSlider(Slider slider, string globalReference)
    {
        slider.value = PlayerPrefs.GetFloat(globalReference, DEFAULT_VOLUME);
        mainMixer.SetFloat(globalReference, 20 * Mathf.Log10(slider.value + float.Epsilon));

        slider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(globalReference, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(globalReference, slider.value);
        });
    }
}
