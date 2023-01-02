using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAnimator : StateMachineBehaviour
{
    private static readonly int _dashAnimator = Animator.StringToHash("dash");

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_dashAnimator, false);
    }
}