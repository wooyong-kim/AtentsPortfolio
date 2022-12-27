using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterProperty, IBattle
{
    Vector3 desireDir = Vector2.zero;
    Vector3 curDir = Vector2.zero;
    float posY;

    public LayerMask EnemyMask = default;
    public CharacterStat myInfo;

    public Transform myHitPosition;
    public Slider myHpBar;
    public Slider myEgBar;

    public float moveSpeed = 10.0f;
    bool IsComboable = false;
    bool Stairs = false;
    int ClickCount = 0;

    float gravity = 2.5f; // 중력 변수
    float yVelocity = 0; // 수직 속력 변수

    public void OnDamage(float dmg)
    {
        if (IsLive)
        {
            myInfo.CurHP -= dmg;
            myHpBar.value = myInfo.CurHP / myInfo.TotalHP;
            if (Mathf.Approximately(myInfo.CurHP, 0.0f))
            {
                myAnim.SetTrigger("Death");
            }
            else
            {
                myAnim.SetTrigger("Damage");
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

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;
    }
    private void FixedUpdate()
    {
        myRigid.velocity = Vector3.zero; // 질량 관성 무시
        // WallCrash();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLive)
        {
            Move();
            StairsMove();
            Gravity();
            Attack();
            Shield();
            Sit();
            Roll();
            myInfo.curEgDelay += Time.deltaTime;
            if (myInfo.curEgDelay >= myInfo.EgDelay)
            {
                myInfo.CurEG += 10.0f * Time.deltaTime;
            }
        }
    }

    void Move()
    {
        desireDir.x = Input.GetAxisRaw("Horizontal");
        desireDir.z = Input.GetAxisRaw("Vertical");

        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * moveSpeed);
        curDir.z = Mathf.Lerp(curDir.z, desireDir.z, Time.deltaTime * moveSpeed);

        myAnim.SetFloat("x", curDir.x);
        myAnim.SetFloat("z", curDir.z);

        float x = Mathf.Round(Mathf.Abs(curDir.x));
        float z = Mathf.Round(Mathf.Abs(curDir.z));

        if (x > 0 || z > 0)
        {
            myAnim.SetBool("IsMoving", true);
        }
        else
        {
            myAnim.SetBool("IsMoving", false);
        }
        // myRigid.AddForce(curDir * moveSpeed);
    }

    void StairsMove()
    {
        if(Stairs && transform.position.y > posY)
        {
            transform.position += new Vector3(0.0f, 0.3f, 0.0f);
            Debug.Log("up");
        }
        posY = transform.position.y;
    }

    void Gravity() // 중력
    {
        yVelocity += gravity * Time.deltaTime;

        myRigid.AddForce(Vector3.down * yVelocity, ForceMode.VelocityChange);
    }
    bool PlayerWall(Vector3 dir)
    {
        return Physics.Raycast(transform.position, dir, 0.33f, LayerMask.GetMask("Wall"));
    }
    void WallCrash()
    {
        if(PlayerWall(transform.right) || PlayerWall(transform.forward) || PlayerWall(-transform.right) || PlayerWall(-transform.forward))
        {
            AnimatorRootMotionMove();
        }
    }

    void AnimatorRootMotionMove() // 벽에 충돌시 켜짐
    {
        transform.position = myAnim.rootPosition;
        transform.rotation = myAnim.rootRotation;
    }

    void Attack()
    {
        if(!(myInfo.CurEG < 30.0f))
        {
            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    myAnim.SetTrigger("ComboAttack");
                    myInfo.curEgDelay = 0.0f;
                    myInfo.CurEG -= 30.0f;
                }
            }

            if (IsComboable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClickCount++;
                    myInfo.curEgDelay = 0.0f;
                    myInfo.CurEG -= 30.0f;
                }
            }
            myEgBar.value = myInfo.CurEG / myInfo.TotalEG;
        }
    }

    void Shield()
    {
        if(!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            if(Input.GetMouseButton(1))
            {
                myAnim.SetBool("Shield", true);
            }
            else
            {
                myAnim.SetBool("Shield", false);
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
        if(!(myInfo.CurEG < 20.0f))
        {
            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myAnim.SetTrigger("Roll");
                    myInfo.curEgDelay = 0.0f;
                    myInfo.CurEG -= 20.0f;
                }
            }
            myEgBar.value = myInfo.CurEG / myInfo.TotalEG;
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPosition.position, 0.5f, EnemyMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(35.0f);
        }
    }

    public void OnSkill()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 2.0f, EnemyMask);
        foreach (Collider col in list)
        {
            IBattle ib = col.GetComponent<IBattle>();
            ib?.OnDamage(50.0f);
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
            if (ClickCount != 1)
            {
                myAnim.SetTrigger("ComboStop");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            Stairs = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stairs"))
        {
            Stairs = false;
        }
    }
}
