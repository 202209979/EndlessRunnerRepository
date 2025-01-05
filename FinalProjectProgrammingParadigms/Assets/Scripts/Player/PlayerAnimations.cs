using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private string currentAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation)
        {
            return; 
        }

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void ShowAnimation(string animation)
    {
        ChangeAnimation(animation);
    }

}
