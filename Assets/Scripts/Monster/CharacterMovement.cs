using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MyAction();
public delegate void MyAction<T>(T t);

public class CharacterMovement : MonoBehaviour
{
    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponent<Rigidbody>();
                if (_rigid == null)
                    _rigid = GetComponentInChildren<Rigidbody>();
            }
            return _rigid;
        }
    }

    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Coroutine coMove = null;
    Coroutine coRot = null;
    public int rand = 0;

    protected void FollowTarget(Transform target, Transform mypos, float MovSpeed, float RotSpeed = 360.0f, MyAction reached = null)
    {
        if (coMove != null) StopCoroutine(coMove);
        coMove = StartCoroutine(FollowingTarget(target, mypos, MovSpeed, RotSpeed, reached));
        if (coRot != null) StopCoroutine(coRot);
    }

    IEnumerator FollowingTarget(Transform target, Transform mypos, float MovSpeed, float RotSpeed, MyAction reached)
    {
        float AttackRange = 0.0f;

        while (target != null)
        {
            rand = Random.Range(0, 100);
            if (rand >= 30) // PunchAttack, SwipingAttack, BreathAttack
            {
                AttackRange = 3.0f;
            }
            else // RunAttack, JumpAttack
            {
                AttackRange = 6.0f;
            }
            AttackRange *= transform.localScale.x;

            Vector3 dir = target.position - mypos.position;
            dir.y = 0.0f;
            float dist = dir.magnitude;            

            if (!myAnim.GetBool("IsAttacking") && dist > AttackRange)
            {
                myAnim.SetBool("IsMoving", true);
                while (dist - AttackRange > Mathf.Epsilon)
                {
                    dir = target.position - mypos.position;
                    dir.y = 0.0f;
                    dist = dir.magnitude;
                    dir.Normalize();
                    Vector3 rot = Vector3.RotateTowards(mypos.forward, dir, RotSpeed * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
                    mypos.rotation = Quaternion.LookRotation(rot);        
                    yield return null;
                }
                myAnim.SetBool("IsMoving", false);
            }
            else
            {
                myAnim.SetBool("IsMoving", false);
                reached?.Invoke();
                new WaitForSeconds(1.0f);
            }
            yield return null;
        }
    }
}
