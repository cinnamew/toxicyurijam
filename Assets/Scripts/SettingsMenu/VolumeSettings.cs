using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider, sfxSlider;

    private void Start()
    {
        SetupSlider(musicSlider, Globals.MUSIC_VOLUME);
        SetupSlider(sfxSlider, Globals.SFX_VOLUME);
    }

    private void SetupSlider(Slider slider, string globalReference)
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
