using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingAudioMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Scrollbar _musicScrollBar;
    [SerializeField] private Scrollbar _soundScrollBar;

    private void Start()
    {
        _musicScrollBar.value = GetVolumeMusic();
        _soundScrollBar.value = GetVolumeSound();
    }

    public float GetVolumeMusic()
    {
        _audioMixer.GetFloat("volumeMusic", out float value);
        return Mathf.Pow(10, value / 20);
    }
    public float GetVolumeSound()
    {
        _audioMixer.GetFloat("volumeEffects", out float value);
        return Mathf.Pow(10, value / 20);
    }

    public void ChangeVolumeMusic(float value)
    {
        _audioMixer.SetFloat("volumeMusic", LinearToLagarithmic(value));
    }
    public void ChangeVolumeSound(float value)
    {
        _audioMixer.SetFloat("volumeEffects", LinearToLagarithmic(value));
    }

    private float LinearToLagarithmic(float linearValue)
    {
        return Mathf.Log10(Mathf.Clamp(linearValue, 0.0001f, 1)) * 20;
    }
}
