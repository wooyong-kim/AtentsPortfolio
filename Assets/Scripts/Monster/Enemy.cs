using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : CharacterMovement, IBattle
{
    public CharacterStat myInfo;
    public LayerMask TargetMask = default;
    public Transform PunchPosition;
    public Transform SwipingPosition;
    public Transform RunAttackPosition;
    public Transform JumpAttackPosition;
    public Transform myHeadPos;
    public Slider myHpBar;
    public enum STATE
    {
        Create, Normal, Battle, Death
    }
    public STATE myState = STATE.Create;
    public AIPerception mySenser = null;
    public MeshFilter myFilter;
    Vector3 StartPos = Vector3.zero;

    float angleRange;
    float distance;
    float BreathAngle;
    float posDistance;

    public void OnDamage(float dmg)
    {
        myInfo.CurHP -= dmg;
        myHpBar.value = myInfo.CurHP / myInfo.TotalHP;
        if (Mathf.Approximately(myInfo.CurHP, 0.0f))
        {
            ChangeState(STATE.Death);
        }
        else
        {
            myAnim.SetTrigger("Damage");
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
                myHpBar.gameObject.SetActive(false);
                myAnim.SetBool("IsMoving", false);
                break;
            case STATE.Battle:
                StopAllCoroutines();
                myHpBar.gameObject.SetActive(true);
                FollowTarget(mySenser.myTarget.transform, transform, myInfo.MoveSpeed, myInfo.RotSpeed, OnAttack);
                break;
            case STATE.Death:
                Destroy(myHpBar.gameObject);
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
                if(mySenser.myTarget != null && !mySenser.myTarget.IsLive)
                {
                    mySenser.OnLostTarget();
                }
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
                int rand = Random.Range(0, 100);
                // int rand = 60;
                if (rand >= 80)
                {
                    myAnim.SetInteger("InputAttack", 0);
                    PunchAttack();
                }
                else if(80 > rand && rand >= 55)
                {
                    myAnim.SetInteger("InputAttack", 1);
                    BreathAttack();
                }
                else if (55 > rand && rand >= 40)
                {
                    myAnim.SetInteger("InputAttack", 2);
                    RunAttack();
                }
                else if (40 > rand && rand >= 10)
                {
                    myAnim.SetInteger("InputAttack", 3);
                    SwipingAttack();
                }
                else
                {
                    myAnim.SetInteger("InputAttack", 4);
                    JumpAttack();
                }
            }
        }
    }

    void PunchAttack()
    {
        Vector3 pos1 = PunchPosition.position + PunchPosition.up * -0.25f;
        Vector3 pos2 = PunchPosition.position + PunchPosition.up * 0.25f;
        // Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.18f, TargetMask);
        Collider[] list = Physics.OverlapSphere(PunchPosition.position, 2.0f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(10.0f);
        }
    }

    void SwipingAttack()
    {
        Vector3 pos1 = SwipingPosition.position + SwipingPosition.up * -0.5f;
        Vector3 pos2 = SwipingPosition.position + SwipingPosition.up * 0.5f;
        // Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.2f, TargetMask);
        Collider[] list = Physics.OverlapSphere(SwipingPosition.position, 2.0f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(20.0f);

        }
    }

    void RunAttack() // ���� �ٽ� �����ؾ� ��
    {
        Vector3 pos1 = RunAttackPosition.position + RunAttackPosition.up * -0.5f;
        Vector3 pos2 = RunAttackPosition.position + RunAttackPosition.up * 0.5f;
        // Collider[] list = Physics.OverlapCapsule(pos1, pos2, 0.2f, TargetMask);
        Collider[] list = Physics.OverlapSphere(RunAttackPosition.position, 2.0f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);

        }
    }

    void JumpAttack()
    {
        Collider[] list = Physics.OverlapSphere(JumpAttackPosition.position, 2.0f, TargetMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(30.0f);
        }
    }

    void BreathAttack()
    {
        // Attackó�� �ִϸ��̼� ������ true, flase�ؼ� fixupdata���� onDamage ����
        // TrianglesMesh();
        if (BreathAngle <= angleRange * 0.5f && posDistance < distance)
        {
            IBattle ib = mySenser.myTarget.transform.GetComponent<IBattle>();
            ib.OnDamage(50.0f);
            // Debug.Log("Hit");
        }
    }

    void BreathRange()
    {
        // ���� ���� ��ŭ RaycasHit�� ���� ��Ʈ�Ǹ� ������ ����? ���� �� ���ؼ� ���� �ȿ� ������ ������ �ָ� �����ε�
        angleRange = 30.0f;
        distance = 2.0f;
        Vector3 playerPos = mySenser.myTarget.transform.position;
        playerPos.y = myHeadPos.transform.position.y;
        BreathAngle = Vector3.Angle(transform.forward, (playerPos - myHeadPos.transform.position).normalized);
        // HeadPos�� �ٶ󺸴� ���� ���� �� HeadPos���� Player�� ���� ���� ���� ����
        posDistance = Vector3.Distance(playerPos, myHeadPos.position);
        // Debug.Log("BreathAngle " + BreathAngle);
    }

    void TrianglesMesh()
    {
        float angleRange = 30.0f;
        float distance = 2.0f;
        Vector3[] myDirs = new Vector3[3];
        Vector3 dir = Vector3.forward * distance;
        myDirs[0] = myHeadPos.localPosition;
        myDirs[1] = myHeadPos.localPosition + Quaternion.AngleAxis(-angleRange / 2.0f, Vector3.up) * dir;
        myDirs[2] = myHeadPos.localPosition + Quaternion.AngleAxis(angleRange / 2.0f, Vector3.up) * dir;

        int[] triangles = new int[] { 0, 1, 2 };

        Mesh _mesh = new Mesh();
        _mesh.vertices = myDirs;
        _mesh.triangles = triangles;
        myFilter.mesh = _mesh;
    }

    bool Changerable()
    {
        return myState != STATE.Death;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySenser.FindTarget += () => { if (Changerable()) ChangeState(STATE.Battle); };
        mySenser.LostTarget += () => { if (Changerable()) ChangeState(STATE.Normal); };

        ChangeState(STATE.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();  
    }

    private void FixedUpdate()
    {
        // BreathRange();
        // TrianglesMesh();
        myRigid.velocity = Vector3.zero;
    }
}
