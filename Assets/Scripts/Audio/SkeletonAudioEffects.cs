using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAudioEffects : ÑharacterAudioEffects
{
    [SerializeField] protected AudioClip _audioClipShot;
    [SerializeField, Range(0f, 1f)] protected float _volumeShot = 1f;
    private void PlayShotShot()
    {
        _audioSourceEffects.PlayOneShot(_audioClipShot, _volumeShot);
    }
}
