using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSystem : InteractableObject
{
    private Animator animator;
    [SerializeField] public float leverState;
    [SerializeField] private Animator doorAnimator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeLeverState(float changeAmount)
    {
        if (leverState + changeAmount > 1)
        {
            leverState = 1f;
        }
        else
        {
            leverState += changeAmount;
        }
        if (leverState + changeAmount < 0)
        {
            leverState = 0;
        }
        else
        {
            leverState += changeAmount;
        }

        animator.SetFloat("LeverState", leverState);

        

        if (leverState >- 0.9f)
        {
            doorAnimator.SetBool("isDoorOpen", true);
        }

        else if (leverState <= 0.1f)
        {
            doorAnimator.SetBool("isDoorOpen", false);
        }
    }
    
}
