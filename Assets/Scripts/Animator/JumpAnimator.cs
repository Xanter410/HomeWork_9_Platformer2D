using UnityEngine;

public class JumpAnimator : StateMachineBehaviour
{
    private static readonly int _jumpAnimator = Animator.StringToHash("jumped");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_jumpAnimator, false);
    }
}
