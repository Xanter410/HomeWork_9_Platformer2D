using UnityEngine;

public class SlimeAudioEffects : �haracterAudioEffects
{
    [SerializeField] protected AudioClip _audioClipDash;
    [SerializeField, Range(0f, 1f)] protected float _volumeDash = 1f;
    private void PlayShotDash()
    {
        _audioSourceEffects.PlayOneShot(_audioClipDash, _volumeDash);
    }
}
