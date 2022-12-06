using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterMovement, IBattle
{
    public CharacterStat myInfo;
    public LayerMask TargetMask = default;
    public Transform PunchPosition;
    public Transform SwipingPosition;
    public Transform RunAttackPosition;
    public Transform JumpAttackPosition;

    public enum STATE
    {
        Create, Normal, Battle, Death
    }
    public STATE myState = STATE.Create;
    public AIPerception mySenser = null;
    public void OnDamage(float dmg)
    {
        myInfo.CurHP -= dmg;
        if(Mathf.Approximately(myInfo.CurHP, 0.0f))
        {
            ChangeState(STATE.Death);
        }
    }
    public bool IsLive
    {
        get
        {
            if(Mathf.Approximately(myInfo.CurHP, 0.0f))
            {
                return false;
            }
            return true;
        }
    }
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch(myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                StopAllCoroutines();
                myAnim.SetBool("IsMoving", false);
                break;
            case STATE.Battle:
                StopAllCoroutines();
                FollowTarget(mySenser.myTarget.transform, myInfo.MoveSpeed, myInfo.RotSpeed, OnAttack);
                break;
            case STATE.Death:
                StopAllCoroutines();
                myAnim.SetTrigger("Death");
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Normal:
                break;
            case STATE.Battle:
                if(!myAnim.GetBool("IsAttacking")) myInfo.curAttackDelay += Time.deltaTime;
                break;
        }
    }
    void OnAttack()
    {
        if(!myAnim.GetBool("IsAttacking"))
        {
            if(myInfo.curAttackDelay >= myInfo.AttackDelay)
            {
                myInfo.curAttackDelay = 0.0f;
                myAnim.SetTrigger("Attack");
            }
        }
    }

    public void PunchAttack()
    {
        Vector3 pos1 = new Vector3(PunchPosition.position.x, PunchPosition.position.y - 0.25f, PunchPosition.position.z);
        Vector3 pos2 = new Vector3(PunchPosition.position.x, PunchPosition.position.y + 0.25f, PunchPosition.position.z);
        Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.18f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(10.0f);
        }
    }

    public void SwipingAttack()
    {
        Vector3 pos1 = new Vector3(SwipingPosition.position.x, SwipingPosition.position.y - 0.5f, SwipingPosition.position.z);
        Vector3 pos2 = new Vector3(SwipingPosition.position.x, SwipingPosition.position.y + 0.5f, SwipingPosition.position.z);
        Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.2f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(20.0f);

        }
    }

    public void RunAttack()
    {
        Vector3 pos1 = new Vector3(RunAttackPosition.position.x, RunAttackPosition.position.y - 0.5f, RunAttackPosition.position.z);
        Vector3 pos2 = new Vector3(RunAttackPosition.position.x, RunAttackPosition.position.y + 0.5f, RunAttackPosition.position.z);
        Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.2f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);

        }
    }

    public void JumpAttack()
    {
        Collider[] list = Physics.OverlapSphere(JumpAttackPosition.position, 2.0f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
}
