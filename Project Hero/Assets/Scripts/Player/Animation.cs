using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        // component set
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //idle upon entry
        animator.SetBool("isIdle", true);
    }

    void Update()
    {
        //Falling
        if (rb.velocity.y <=  -0.1f)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", false);

            Debug.Log("Falling");
        }
        //Jumping
        else if(rb.velocity.y >= 0.1f)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", true);

            Debug.Log("Jumping");
        }
        //Idle
        else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isJumping", false);

            Debug.Log("Idle");
        }

        //Dying not implemented

    }
}
