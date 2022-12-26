using UnityEngine;

public class AttackAnimator : StateMachineBehaviour
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(_attacked, 0);
    }
}
