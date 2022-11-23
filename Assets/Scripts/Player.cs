using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProperty
    
{
    float x;
    float y;
    float moveSpeed;
    float inputMagnitude;
    bool Walking = false;
    bool isSprinting = false;
    public float walkSpeed = 2.0f;
    public float runningSpeed = 4.0f;
    public float sprintSpeed = 6.0f;

    public PlayerAnimator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        MoveSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandle();
        playerAnimator.UpdateAnimator();
    }

    void InputHandle()
    {
        MoveInput();
        SprintInput();
    }

    void MoveSpeed()
    {
        if(!isSprinting)
        {
            if (Walking)
            {
                inputMagnitude = 0.5f;
                moveSpeed = walkSpeed;
            }
            else
            {
                inputMagnitude = 1.0f;
                moveSpeed = runningSpeed;
            }
        }
        else
        {
            inputMagnitude = 1.5f;
            moveSpeed = sprintSpeed;
        }

        myAnim.SetFloat("IsSprinting", inputMagnitude);
    }
    void MoveInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    void SprintInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnim.SetBool("IsSprinting", true);
            isSprinting = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            myAnim.SetBool("IsSprinting", false);
            isSprinting = false;
        }
    }
}
