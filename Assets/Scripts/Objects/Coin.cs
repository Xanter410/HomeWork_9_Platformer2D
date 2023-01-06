using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : Interactable
{
    private static readonly int DeadedAnimator = Animator.StringToHash("deaded");

    [SerializeField] private CoinCounter _coinCounter;
    private Animator _animator;
    bool _isActive = false;

    private UnityAction _onCoinPickup;
    public event UnityAction OnCoinPickup
    {
        add { _onCoinPickup += value; }
        remove { _onCoinPickup -= value; }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Activate(PlayerController player)
    {
        if (!_isActive)
        {
            _isActive = true;

            _coinCounter.CoinScore++;

            _animator.SetBool(DeadedAnimator, true);
            _onCoinPickup?.Invoke();

            StartCoroutine(DeleteCoin());
        }
    }
    IEnumerator DeleteCoin()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
