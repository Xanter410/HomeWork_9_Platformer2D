using System.Collections;
using UnityEngine;

public class Coin : Interactable
{
    private static readonly int DeadedAnimator = Animator.StringToHash("deaded");

    [SerializeField] private GameEvent _onCoinPickUp;
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
        _animator.SetBool(DeadedAnimator, true);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
