using UnityEngine;

public class AttackAnimator : StateMachineBehaviour
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_attacked, false);
    }
}
