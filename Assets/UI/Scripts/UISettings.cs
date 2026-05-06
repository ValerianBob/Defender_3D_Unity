using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _generalSlider;

    private void Start()
    {
        _musicSlider.value = Settings.instance.MusicVolume;
        _generalSlider.value = Settings.instance.GeneralVolume;

        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _generalSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMusicVolumeChanged(float value)
    {
        Settings.instance.MusicVolume = value;
    }

    private void OnSFXVolumeChanged(float value)
    {
        Settings.instance.GeneralVolume = value;
    }

    private void OnDestroy()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        _generalSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }
}
