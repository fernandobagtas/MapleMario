using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //source:https://github.com/Brackeys/2D-Character-Controller
    //modified to have double jumping and dashing movements working with animation signals
    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    public float runSpeed = 40f;

    bool jumpCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get float value for horizontal movement
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); //absolute because running on the left counts as negative speed

        //Check if space was pressed to jump
        //Edit > Project Settings > Input Manager > Axes for other input options for later programming
        if (Input.GetButtonDown("Jump"))
        {
            jumpCheck = true;
            //animator.SetBool("isJumping", true);
            //jumpCount++;
            //switch (jumpCount) //select which animation to play
            //{
            //    case 1:
            //        Debug.Log("jump");
            //        jumpCheck = true;
            //        animator.SetBool("isJumping", true);
            //        break;
            //    case 2:
            //        Debug.Log("JUMP");
            //        jumpCheck = true;
            //        airJumpCheck = true;
            //        animator.SetBool("isDJumping", true);
            //        break;
            //    default:
            //        //jumpCheck = true;
            //        Debug.Log("STOP JUMPING");
            //        break;
            //}
        }
    }

    public void onLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isDJumping", false);
    }
    
    void FixedUpdate()
    {
        //Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jumpCheck);
        jumpCheck = false;
    }
}
