using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEditor.PlayerSettings;

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
    public int rand;
    int _attacknum = 0;
    public int AttackNum
    {
        get => _attacknum;
        set => _attacknum = value;
    }
    int _attacknumroot = 5;
    public int AttackNumRoot
    {
        get => _attacknumroot;
        set => _attacknumroot = value;
    }

    protected void FollowTarget(Transform target, Transform mypos, float MovSpeed, float RotSpeed = 360.0f, MyAction reached = null)
    {
        if (coMove != null) StopCoroutine(coMove);
        coMove = StartCoroutine(FollowingTarget(target, mypos, MovSpeed, RotSpeed, reached));
        if (coRot != null) StopCoroutine(coRot);
    }

    void LookTaget(Transform mypos, Vector3 dir, float RotSpeed)
    {
        Vector3 rot = Vector3.RotateTowards(mypos.forward, dir, RotSpeed * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
        mypos.rotation = Quaternion.LookRotation(rot);
    }

    IEnumerator FollowingTarget(Transform target, Transform mypos, float MovSpeed, float RotSpeed, MyAction reached)
    {
        float AttackRange = 0.0f;
        while (target != null)
        {
            AttackNumRoot = myAnim.GetInteger("AttackNum");

            if (!myAnim.GetBool("IsAttacking"))
            {
                rand = Random.Range(0, 100);

                if (rand >= 80)
                {
                    AttackNum = 0; // PunchAttack
                }
                else if (80 > rand && rand >= 50)
                {
                    AttackNum = 1; // SwipingAttack
                }
                else if (50 > rand && rand >= 30)
                {
                    AttackNum = 2; // BreathAttack
                }
                else if (30 > rand && rand >= 20)
                {
                    AttackNum = 3; // RunAttack
                }
                else
                {
                    AttackNum = 4; // JumpAttack
                }

                if (AttackNum <= 2) // PunchAttack, SwipingAttack, BreathAttack
                {
                    AttackRange = 3.0f;
                }
                else // RunAttack, JumpAttack
                {
                    AttackRange = 100.0f;
                }
                AttackRange *= transform.localScale.x;
            }

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
                    LookTaget(mypos, dir, RotSpeed);
                    yield return null;
                }
                myAnim.SetBool("IsMoving", false);
            }
            else
            {
                if(!myAnim.GetBool("IsAttacking"))
                {
                    LookTaget(mypos, dir, RotSpeed);
                }
                myAnim.SetBool("IsMoving", false);
                reached?.Invoke();
            }
            yield return null;
        }
    }
}
