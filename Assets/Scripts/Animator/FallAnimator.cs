using UnityEngine;

public class FallAnimator : StateMachineBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerAudioEffects _playerAudioEffects;

    private void OnEnable()
    {
       _playerAudioEffects = _player.GetComponent<PlayerAudioEffects>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerAudioEffects.PlayShotAudioFall();
    }
}
