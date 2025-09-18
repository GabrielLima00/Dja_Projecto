using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;

    private void Start()
    {
        settingsPanel.SetActive(false);

        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
