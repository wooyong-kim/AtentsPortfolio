using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterProperty, IBattle
{
    Vector3 desireDir = Vector2.zero;
    Vector3 curDir = Vector2.zero;
    Vector3 moveDirection = Vector3.zero; // 이동 방향

    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public CharacterStat myInfo;

    public Transform groundCheck;
    public Transform myHitPosition;
    public Slider myHpBar;
    public Slider myEgBar;

    public float moveSpeed = 10.0f;
    bool IsComboable = false; // 콤보 공격
    bool isMove = false; // 움직임
    bool hitMiss = false;
    bool shield = false;
    int ClickCount = 0;
    float x, z; // 캐릭터의 Horizontal Vertical 값
    public float forceGravity = 50f; // 캐릭터 중력 값 

    public GameObject death;
    public AudioClip[] playerSound;

    public void OnDamage(float dmg)
    {
        if (IsLive && !hitMiss)
        {
            if (shield && myInfo.CurSP >= dmg)
            {
                myInfo.CurHP -= dmg * 0.5f;
                myInfo.CurSP -= dmg;
                myInfo.curSpDelay = 0.0f;
                myEgBar.value = myInfo.CurSP / myInfo.MaxSP;
                PlayerSpeeker.PlayOneShot(playerSound[0]);
            }
            else
            {
                myInfo.CurHP -= dmg;
            }
            if (Mathf.Approximately(myInfo.CurHP, 0.0f))
            {
                myHpBar.value = 0;
                myAnim.SetTrigger("Death");
                Death();
            }
            else
            {
                myAnim.SetTrigger("Damage");
                StartCoroutine(MissTime(0.5f));
            }
        }
    }
    public bool IsLive
    {
        get
        {
            if (Mathf.Approximately(myInfo.CurHP, 0.0f))
            {
                return false;
            }
            return true;
        }
    }

    public AudioSource _speaker = null;
    public AudioSource PlayerSpeeker
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

    // Start is called before the first frame update
    void Start()
    {
        death.SetActive(false);
        if (GameObject.Find("SettingJson").GetComponent<SettingJson>().set.Save == 0)
        {
            MyCharacter.Inst.NewData();
        }
        else if (GameObject.Find("SettingJson").GetComponent<SettingJson>().set.Save == 1)
        {
            MyCharacter.Inst.LoadData();
        }
        myInfo = MyCharacter.Inst.playerInfo.playerStat;
    }

    // Update is called once per frame
    void Update()
    {
        // UI가 비활성화일 때
        if (IsLive && !UI.inventoryActivatedInven && !UI.inventoryActivatedOption && !UI.levelActivate)
        {
            Move();
            Gravity();
            Attack();
            Shield();
            Sit();
            Roll();
            myHpBar.value = myInfo.CurHP / myInfo.MaxHp;
            myInfo.curSpDelay += Time.deltaTime;  
            if (myInfo.curSpDelay >= myInfo.SpDelay)
            {
                myInfo.CurSP += 10.0f * Time.deltaTime;
            }
        }

        if(FileManager.Inst.StatChange) // LEVEL UP
        {
            myInfo = MyCharacter.Inst.playerInfo.playerStat;
            myInfo.CurHP = myInfo.MaxHp;
            myInfo.CurSP = myInfo.MaxSP;
            FileManager.Inst.StatChange = false;
        }
    }

    void Move()
    {
        if(!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            desireDir.x = Input.GetAxisRaw("Horizontal");
            desireDir.z = Input.GetAxisRaw("Vertical");

            curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * moveSpeed);
            curDir.z = Mathf.Lerp(curDir.z, desireDir.z, Time.deltaTime * moveSpeed);

            // 이동 방향 계산
            moveDirection = transform.forward * curDir.z + transform.right * curDir.x;
            
            myRigid.velocity = moveDirection * moveSpeed;

            myAnim.SetFloat("x", curDir.x);
            myAnim.SetFloat("z", curDir.z);

            x = Mathf.Round(Mathf.Abs(curDir.x));
            z = Mathf.Round(Mathf.Abs(curDir.z));

            if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(z, 0.0f))
            {
                myAnim.SetBool("IsMoving", true);
                isMove = true;
            }
            else
            {
                myAnim.SetBool("IsMoving", false);
                isMove = false;
            }
        }
    }

    void Gravity()
    {
        // 캐릭터 바로 아래에 지면이 있는지 확인
        if(!Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hitInfo, 0.5f, groundLayer))
        {
            // myRigid.velocity = myRigid.velocity.y * Vector3.down * moveSpeed;
        }    
    }

    void Death()
    {
        FileManager.Inst.SaveData(MyCharacter.Inst.playerInfo.playerStat); // Player.Json에 데이터 저장
        death.SetActive(true);
    }

    void Attack()
    {
        if(!(myInfo.CurSP < 15.0f))
        {
            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    myAnim.SetTrigger("ComboAttack");
                    myInfo.curSpDelay = 0.0f;
                    myInfo.CurSP -= 15.0f;
                }
            }

            if (IsComboable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClickCount++;
                    myInfo.curSpDelay = 0.0f;
                }
            }
            myEgBar.value = myInfo.CurSP / myInfo.MaxSP;
        }
    }

    void Shield()
    {
        if(!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            if(Input.GetMouseButton(1))
            {
                myAnim.SetBool("Shield", true);
                shield = true;
            }
            else
            {
                myAnim.SetBool("Shield", false);
                shield = false;
            }
        }
    }

    void Sit()
    {
        if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(!myAnim.GetBool("Sit"))
                {
                    myAnim.SetBool("Sit", true);
                    
                }                   
                else
                {
                    myAnim.SetBool("Sit", false);
                }      
            }
        }
    }

    void Roll()
    {
        if(!(myInfo.CurSP < 20.0f))
        {
            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myAnim.SetTrigger("Roll");
                    StartCoroutine(MissTime(0.5f));
                    myInfo.curSpDelay = 0.0f;
                    myInfo.CurSP -= 20.0f;

                    // 캐릭터가 이동 중이지 않으면
                    if(Mathf.Approximately(x, 0.0f) && Mathf.Approximately(z, 0.0f))
                    {
                        myRigid.velocity = transform.forward * moveSpeed;
                    }
                    // 캐릭터가 이동 중이면
                    else
                    {
                        myRigid.velocity = moveDirection * moveSpeed;
                    }
                }
            }
            myEgBar.value = myInfo.CurSP / myInfo.MaxSP;
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPosition.position, 1.0f * transform.localScale.x, enemyLayer);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(MyCharacter.Inst.playerInfo.playerStat.AttackDG);
        }
    }

    public void OnComboCheck(bool v)
    {
        IsComboable = v;
        if (v)
        {
            //ComboCheckStart
            ClickCount = 0;
        }
        else
        {
            //ComboCheckEnd
            if (ClickCount == 0)
            {
                myAnim.SetTrigger("ComboStop");
            }
        }
    }

    public void OnAttackSound()
    {
        PlayerSpeeker.PlayOneShot(playerSound[1]);
    }

    public void OnComboAttackSound()
    {
        if(ClickCount > 0)
        {
            PlayerSpeeker.PlayOneShot(playerSound[1]);
        }     
    }

    // misstime 값 만큼 공격을 회피
    IEnumerator MissTime(float misstime)
    {
        hitMiss = true;
        yield return new WaitForSeconds(misstime);
        hitMiss = false;
    }
}
