using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : CharacterMovement
{
    Vector3 moveDelta = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.parent.Translate(moveDelta, Space.World);
        moveDelta = Vector3.zero;
    }
    private void OnAnimatorMove()
    {
        if(AttackNum == 3)
        {
            moveDelta = Vector3.forward * 10.0f;
        }
        else
        {
            moveDelta += GetComponent<Animator>().deltaPosition;
            transform.parent.Rotate(GetComponent<Animator>().deltaRotation.eulerAngles, Space.World);
        }
    }
}
