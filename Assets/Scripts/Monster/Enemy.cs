using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Enemy : CharacterMovement, IBattle
{
    public CharacterStat myInfo;
    public LayerMask TargetMask = default;
    public Transform PunchPosition;
    public Transform SwipingPosition;
    public Transform JumpAttackPosition;
    public Transform myHeadPos;
    public Slider myHpBar;
    public SettingJson setjson;
    public AudioClip[] EnemySound;

    public enum STATE
    {
        Create, Normal, Battle, Death
    }
    public STATE myState = STATE.Create;
    public AIPerception mySenser = null;
    public MeshFilter myFilter;

    public void OnDamage(float dmg)
    {
        myInfo.CurHP -= dmg;
        myHpBar.value = myInfo.CurHP / myInfo.MaxHp;
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

    public AudioSource _speaker = null;
    public AudioSource EnemySpeeker
    {
        get
        {
            if (_speaker == null)
            {
                _speaker = GetComponent<AudioSource>();
            }
            return _speaker;
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
                MyCharacter.Inst.playerInfo.playerStat.SoulS += 1000; // 죽으면 캐릭터 소울 증가
                EnemySpeeker.PlayOneShot(EnemySound[5]);
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
        if(!myAnim.GetBool("IsAttacking") && myInfo.curAttackDelay >= myInfo.AttackDelay)
        {
            myInfo.curAttackDelay = 0.0f;
            myAnim.SetTrigger("Attack");
                
            if (AttackNum == 0)
            {
                myAnim.SetInteger("InputAttack", 0); // PunchAttack
            }
            else if(AttackNum == 1)
            {
                myAnim.SetInteger("InputAttack", 1); // SwipingAttack
            }
            else if (AttackNum == 2)
            {
                myAnim.SetInteger("InputAttack", 2); // BreathAttack              
            }
            else
            {
                myAnim.SetInteger("InputAttack", 3); // JumpAttack
            }
        }
    }
    
    bool Changerable()
    {
        return myState != STATE.Death;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("SettingJson").GetComponent<SettingJson>().set.diffculty == 0) // Easy 난이도 선택시
        {
            MyCharacter.Inst.LoadEnemyEasyData(); // EnemyEasy.Json 파일을 읽어옴
            myInfo = MyCharacter.Inst.enemyInfo.playerStat;
        }
        else if(GameObject.Find("SettingJson").GetComponent<SettingJson>().set.diffculty == 1) // Hard 난이도 선택시
        {   
            MyCharacter.Inst.LoadEnemyHardData(); // EnemyHard.Json 파일을 읽어옴
            myInfo = MyCharacter.Inst.enemyInfo.playerStat;
        }

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
        myRigid.velocity = Vector3.zero;
    }

    public void OnPunchAttackSound()
    {
        EnemySpeeker.PlayOneShot(EnemySound[0]);
    }

    public void OnSwipingAttackSound()
    {
        EnemySpeeker.PlayOneShot(EnemySound[1]);
    }

    public void OnBreathAttackSound()
    {
        EnemySpeeker.PlayOneShot(EnemySound[2]);
    }

    public void OnJumpAttackStartSound()
    {
        EnemySpeeker.PlayOneShot(EnemySound[3]);
    }

    public void OnJumpAttackEndSound()
    {
        EnemySpeeker.PlayOneShot(EnemySound[4]);
    }
}
