using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider masterSlider, musicSlider, sfxSlider;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(Globals.MASTER_VOLUME, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(Globals.MUSIC_VOLUME, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Globals.SFX_VOLUME, 1f);

        mainMixer.SetFloat(Globals.MASTER_VOLUME, 20 * Mathf.Log10(masterSlider.value + float.Epsilon));
        mainMixer.SetFloat(Globals.MUSIC_VOLUME, 20 * Mathf.Log10(musicSlider.value + float.Epsilon));
        mainMixer.SetFloat(Globals.SFX_VOLUME, 20 * Mathf.Log10(sfxSlider.value + float.Epsilon));

        masterSlider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(Globals.MASTER_VOLUME, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(Globals.MASTER_VOLUME, masterSlider.value);
        });

        musicSlider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(Globals.MUSIC_VOLUME, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(Globals.MUSIC_VOLUME, musicSlider.value);
        });

        sfxSlider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(Globals.SFX_VOLUME, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(Globals.SFX_VOLUME, sfxSlider.value);
        });
    }
}
