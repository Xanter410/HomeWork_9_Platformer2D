using UnityEngine;

public abstract class ÑharacterAudioEffects : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSourceEffects;
    [SerializeField] protected AudioClip _audioClipMove;
    [SerializeField, Range(0f, 1f)] protected float _volumeMove = 1f;
    [SerializeField] protected AudioClip _audioClipDead;
    [SerializeField, Range(0f, 1f)] protected float _volumeDead = 1f;
    [SerializeField] protected AudioClip _audioClipTakeHit;
    [SerializeField, Range(0f, 1f)] protected float _volumeTakeHit = 1f;

    protected AudioClip RandomClip(AudioClip[] audioClips)
    {
        return audioClips[Random.Range(0, audioClips.Length - 1)];
    }

    protected void PlayShotAudioMove()
    {
        _audioSourceEffects.PlayOneShot(_audioClipMove, _volumeMove);
    }
    protected void PlayShotAudioDead()
    {
        _audioSourceEffects.PlayOneShot(_audioClipDead, _volumeDead);
    }
    protected void PlayShotAudioTakeHit()
    {
        _audioSourceEffects.PlayOneShot(_audioClipTakeHit, _volumeTakeHit);
    }
}
