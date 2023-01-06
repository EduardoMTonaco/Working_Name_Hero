using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandle : MonoBehaviour
{
    public Animation Animation;
    public Animator Animator;

    public void AnimatonPlayRun()
    {
        if (Animation != null)
        {
            Animation.Play("run");
        }  
        if (Animator != null)
        {
            Animator.SetBool("Move", true);
            Animator.SetBool("Attack", false);
        }
            
    }
    public void AnimationPlayAttack()
    {
        if(Animation != null)
        {
            Animation.Play("attack1");
        }
        if(Animator != null)
        {
            Animator.SetBool("Move", false);
            Animator.SetBool("Attack", true);
        }

        
    }
    public void AnimationPlayStop()
    {
        if (Animation != null)
        {
            Animation.PlayQueued("idle");
        }

    }
    public void AnimationPlayDeath()
    {
        if (Animation != null)
        {
            Animation.Play("death1");
        }
        if (Animator != null)
        {
            Animator.SetBool("Death", true);
        }
    }
    public void AnimationPlayIdle()
    {

        if (Animation != null)
        {
            Animation.PlayQueued("idle");
        }
        if (Animator != null)
        {
                Animator.SetBool("Move", false);
                Animator.SetBool("Attack", false);
        }
    }
}
