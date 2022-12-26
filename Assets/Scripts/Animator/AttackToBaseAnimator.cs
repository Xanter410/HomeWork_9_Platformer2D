using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToBaseAnimator : StateMachineBehaviour
{
    private static readonly int _attacked = Animator.StringToHash("attacked");

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(_attacked, -1);
    }
}
