using System.Collections;
using UnityEngine;

public class Coin : Interactable
{
    private static readonly int DeadedAnimator = Animator.StringToHash("deaded");

    [SerializeField] private GameEvent _onCoinPickUp;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioCoinPickUp;
    [SerializeField, Range(0f, 1f)] protected float _volumeFall = 1f;
    private Animator _animator;
    bool _isActive = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Activate(PlayerController player)
    {
        if (!_isActive)
        {
            _isActive = true;

            _onCoinPickUp.TriggerEvent();

            StartCoroutine(DeleteCoin());
        }
    }
    IEnumerator DeleteCoin()
    {
        _audioSource.PlayOneShot(_audioCoinPickUp);
        _animator.SetBool(DeadedAnimator, true);

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
