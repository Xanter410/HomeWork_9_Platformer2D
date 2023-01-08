using UnityEngine;

public class PlayAudioSceneTransition : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipOpening;
    [SerializeField, Range(0f, 1f)] protected float _volumeOpening = 1f;
    [SerializeField] private AudioClip _audioClipEnding;
    [SerializeField, Range(0f, 1f)] protected float _volumeEnding = 1f;

    private void PlayClipOpening()
    {
        _audioSource.PlayOneShot(_audioClipOpening, _volumeOpening);
    }

    private void PlayClipEnding()
    {
        _audioSource.PlayOneShot(_audioClipEnding, _volumeEnding);
    }
}
