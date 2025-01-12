using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (volumeSlider == null)
        {
            Debug.LogError("Volume Slider is not assigned in the Inspector!");
            return; // Evitar continuar si el Slider no está asignado
        }
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;
        SoundManager.Instance.SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float value)
    {
        SoundManager.Instance.SetVolume(value);
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }
}

