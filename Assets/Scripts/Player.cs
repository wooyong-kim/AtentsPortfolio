using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProperty
    
{
    Vector2 desireDir = Vector2.zero;
    Vector2 curDir = Vector2.zero;

    public float moveSpeed = 1.0f;
    public float inputMagnitude;
    public bool isSprinting = false;
    public float movementSmooth = 6f;
    public float walkSpeed = 2.0f;
    public float runningSpeed = 4.0f;
    public float sprintSpeed = 6.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        desireDir.x = Input.GetAxis("Horizontal");
        desireDir.y = Input.GetAxis("Vertical");

        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * 10.0f);
        curDir.y = Mathf.Lerp(curDir.y, desireDir.y, Time.deltaTime * 10.0f);

        myAnim.SetFloat(AniParameters.InputVertical, curDir.x);
        myAnim.SetFloat(AniParameters.InputMagnitude, curDir.y);
        // MoveInput();
    }

    private void OnAnimatorMove()
    {
        MoveCharacter();
    }

    void MoveInput()
    {
        desireDir.x = Input.GetAxis("Horizontal");
        desireDir.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            inputMagnitude = 1.5f;
            moveSpeed *= 2.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            inputMagnitude = desireDir.x;
            moveSpeed *= 0.5f;
        }

        myAnim.SetFloat(AniParameters.InputVertical, curDir.y);
        myAnim.SetFloat(AniParameters.InputHorizontal, curDir.x * moveSpeed);
        myAnim.SetFloat(AniParameters.InputMagnitude, curDir.x * moveSpeed);
    }

    void MoveCharacter()
    {
        if(curDir == Vector2.zero)
        {
            transform.position = myAnim.rootPosition;
            transform.rotation = myAnim.rootRotation;
        }
        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, 1.0f * Time.deltaTime);
        curDir.y = Mathf.Lerp(curDir.y, desireDir.y, 1.0f * Time.deltaTime);
    }

    public static partial class AniParameters
    {
        public static int InputHorizontal = Animator.StringToHash("InputHorizontal");
        public static int InputVertical = Animator.StringToHash("InputVertical");
        public static int InputMagnitude = Animator.StringToHash("InputMagnitude");
        public static int IsGrounded = Animator.StringToHash("IsGrounded");
        public static int IsSprinting = Animator.StringToHash("IsSprinting");
        public static int GroundDistance = Animator.StringToHash("GroundDistance");
    }
}
