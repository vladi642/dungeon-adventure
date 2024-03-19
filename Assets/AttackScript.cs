using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}



    private bool isAttacking = false;

    // This method is called on every frame while the state is active
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check for the left mouse button click and not currently attacking
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            // Set the "AttackTrigger" parameter to start the attack animation
            animator.SetBool("AttackTrigger", true);

            // Set the attacking flag to true to prevent repeating attacks
            isAttacking = true;
        }

        // If the animator is not in the attack state, reset the attacking flag
        if (!animator.GetCurrentAnimatorStateInfo(layerIndex).IsName("Player_Bow_attack")) // Change "Player_Bow_attack" to your attack state name
        {
            // Reset the "AttackTrigger" parameter
            animator.SetBool("AttackTrigger", false);

            // Reset the attacking flag
            isAttacking = false;
        }
    }
}
