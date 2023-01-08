using UnityEngine;

public class PlayerAudioEffects : ÑharacterAudioEffects
{
    [SerializeField] private AudioClip[] _audioClipsSwordSwing;
    [SerializeField, Range(0f, 1f)] protected float _volumeSwordSwing = 1f;
    [SerializeField] private AudioClip[] _audioClipsJump;
    [SerializeField, Range(0f, 1f)] protected float _volumeJump = 1f;
    [SerializeField] private AudioClip[] _audioClipsFall;
    [SerializeField, Range(0f, 1f)] protected float _volumeFall = 1f;

    private void PlayShotAudioSwordSwing()
    {
        _audioSourceEffects.PlayOneShot(RandomClip(_audioClipsSwordSwing), _volumeSwordSwing);
    }

    private void PlayShotAudioJump()
    {
        _audioSourceEffects.PlayOneShot(RandomClip(_audioClipsJump), _volumeJump);
    }
    public void PlayShotAudioFall()
    {
        _audioSourceEffects.PlayOneShot(RandomClip(_audioClipsFall), _volumeFall);
    }
}
