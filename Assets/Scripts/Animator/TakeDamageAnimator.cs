using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageAnimator : StateMachineBehaviour
{
    private static readonly int _takeDamageAnimator = Animator.StringToHash("takeDamage");

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(_takeDamageAnimator, false);
    }
}
