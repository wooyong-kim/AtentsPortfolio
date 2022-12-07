using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MyAction();
public delegate void MyAction<T>(T t);

public class CharacterMovement : MonoBehaviour
{
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

    protected void FollowTarget(Transform target, Transform mypos, float MovSpeed = 1.0f, float RotSpeed = 360.0f, MyAction reached = null)
    {
        if (coMove != null) StopCoroutine(coMove);
        coMove = StartCoroutine(FollowingTarget(target, mypos, MovSpeed, RotSpeed, reached));
        if (coRot != null) StopCoroutine(coRot);
    }

    IEnumerator FollowingTarget(Transform target, Transform mypos, float MovSpeed, float RotSpeed, MyAction reached)
    {
        float AttackRange = 1.1f;
        while (target != null)
        {
            //transform.LookAt(target.position);
            Vector3 dir = target.position - mypos.position;
            dir.y = 0.0f;
            float dist = dir.magnitude;

            Vector3 rot = Vector3.RotateTowards(mypos.forward, dir, RotSpeed * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
            mypos.rotation = Quaternion.LookRotation(rot);

            if (!myAnim.GetBool("IsAttacking") && dist > AttackRange + 0.1f)
            {
                myAnim.SetBool("IsMoving", true);
                dir.Normalize();
                float delta = MovSpeed * Time.deltaTime;
                if (delta > dist - AttackRange)
                {
                    delta = dist - AttackRange;
                    myAnim.SetBool("IsMoving", false);
                }
                mypos.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("IsMoving", false);
                reached?.Invoke();
            }
            yield return null;
        }
    }
}
