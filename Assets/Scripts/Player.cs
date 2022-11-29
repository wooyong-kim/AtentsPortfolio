using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProperty
{
    public float moveSpeed = 1.0f;
    public float inputMagnitude;
    public bool isSprinting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Lerp(myAnim.GetFloat("x"), Input.GetAxisRaw("Horizontal"), Time.deltaTime * moveSpeed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), Input.GetAxisRaw("Vertical"), Time.deltaTime * moveSpeed);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
        // MoveInput();
    }

    void MoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            inputMagnitude = 1.5f;
            moveSpeed *= 2.0f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            inputMagnitude = x;
            moveSpeed *= 0.5f;
        }

        x = Mathf.Lerp(myAnim.GetFloat("x"), x, moveSpeed * Time.deltaTime);
        y = Mathf.Lerp(myAnim.GetFloat("y"), y, moveSpeed * Time.deltaTime);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
    }
}
