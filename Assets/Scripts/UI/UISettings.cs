using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSfx;
    [SerializeField] private Slider sliderUi;


    private void Awake()
    {
        sliderMaster.onValueChanged.AddListener(ChangeMasterVolume);
        sliderMusic.onValueChanged.AddListener(ChangeMusicVolume);
        sliderSfx.onValueChanged.AddListener(ChangeSfxVolume);
        sliderUi.onValueChanged.AddListener(ChangeUiVolume);
    }
    private void Start()
    {
        LoadVolume("VolumeMaster", sliderMaster);
        LoadVolume("VolumeMusic", sliderMusic);
        LoadVolume("VolumeSfx", sliderSfx);
        LoadVolume("VolumeUI", sliderUi);
    }
    private void OnDestroy()
    {
        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderMusic.onValueChanged.RemoveAllListeners();
        sliderSfx.onValueChanged.RemoveAllListeners();
        sliderUi.onValueChanged.RemoveAllListeners();
    }

    void LoadVolume(string key, Slider slider)
    {
        float vol = PlayerPrefs.GetFloat(key, 0);
        slider.value = vol;
        mixer.SetFloat(key, vol);
    }
    private void ChangeMasterVolume(float currentValue)
    {
        mixer.SetFloat("VolumeMaster", currentValue);
        PlayerPrefs.SetFloat("VolumeMaster", currentValue);
    }
    private void ChangeMusicVolume(float currentValue)
    {
        mixer.SetFloat("VolumeMusic", currentValue);
        PlayerPrefs.SetFloat("VolumeMusic", currentValue);
    }
    private void ChangeSfxVolume(float currentValue)
    {
        mixer.SetFloat("VolumeSfx", currentValue);
        PlayerPrefs.SetFloat("VolumeSfx", currentValue);
    }
    private void ChangeUiVolume(float currentValue)
    {
        mixer.SetFloat("VolumeUI", currentValue);
        PlayerPrefs.SetFloat("VolumeUI", currentValue);
    }

}
