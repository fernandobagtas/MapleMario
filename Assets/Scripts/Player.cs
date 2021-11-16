using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //source:https://github.com/Brackeys/2D-Character-Controller
    //modified to have double jumping
    public CharacterController2D controller;
    public Animator animator;

    float horizontalMove = 0f;

    public float runSpeed = 40f;

    bool jumpCheck = false;
    int jumpCount; //keeps track of how many times the player has jumped


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get float value for horizontal movement
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Check if space was pressed to jump
        //Edit > Project Settings > Input Manager > Axes for other input options for later programming
        if (Input.GetButtonDown("Jump"))
        {
            jumpCheck = true;
            animator.SetBool("isJumping", true);
            //switch (jumpCount) //select which animation to play
            //{
            //    case 1:
            //        animator.SetBool("isJumping", true);
            //        break;
            //    case 2:
            //        animator.SetBool("isDJumping", true);
            //        break;
            //}
        }
    }

    public void onLanding()
    {
        //Debug.Log("landed");
        animator.SetBool("isJumping", false);
        jumpCount = 0; //reset counter once player has landed on the ground
    }
    
    void FixedUpdate()
    {
        //Move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jumpCheck);
        jumpCheck = false;
    }
}
