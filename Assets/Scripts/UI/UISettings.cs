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
    private void OnDestroy()
    {
        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderMusic.onValueChanged.RemoveAllListeners();
        sliderSfx.onValueChanged.RemoveAllListeners();
        sliderUi.onValueChanged.RemoveAllListeners();
    }

    private void ChangeMasterVolume(float currentValue)
    {
        mixer.SetFloat("VolumeMaster", currentValue);
    }
    private void ChangeMusicVolume(float currentValue)
    {
        mixer.SetFloat("VolumeMusic", currentValue);
    }
    private void ChangeSfxVolume(float currentValue)
    {
        mixer.SetFloat("VolumeSfx", currentValue);
    }
    private void ChangeUiVolume(float currentValue)
    {
        mixer.SetFloat("VolumeUi", currentValue);
    }

}
