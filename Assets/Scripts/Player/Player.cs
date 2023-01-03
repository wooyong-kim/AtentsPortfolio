using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterProperty, IBattle
{
    Vector3 desireDir = Vector2.zero;
    Vector3 curDir = Vector2.zero;

    public LayerMask EnemyMask = default;
    public CharacterStat myInfo;

    public Transform myHitPosition;
    public Slider myHpBar;
    public Slider myEgBar;

    public float moveSpeed = 10.0f;
    bool IsComboable = false;
    bool isMove = false;
    int ClickCount = 0;

    float gravity = 2.5f; // 중력 변수
    float yVelocity = 0; // 수직 속력 변수

    public Transform myKnee;
    public Transform myFoot;
    RaycastHit slopHit;
    public float maxSlopeAngle = 30.0f;

    public Transform nextFrameRaycast;
    public Transform groundCheck;

    public void OnDamage(float dmg)
    {
        if (IsLive)
        {
            myInfo.CurHP -= dmg;          
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
    }
    private void FixedUpdate()
    {
        //myRigid.velocity = Vector3.zero; // 질량 관성 무시
    }
    Vector3 AdjustDirectionToSlope(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopHit.normal).normalized;
    }

    float CalculateNextFrameGroundAngle(float moveSpeed) // 다음 프레임의 지면 각도
    {
        var nextFramePlayerPosition = nextFrameRaycast.position + desireDir * moveSpeed * Time.deltaTime;
        if (Physics.Raycast(nextFramePlayerPosition, Vector3.down, out RaycastHit hitInfo, 2.0f, LayerMask.GetMask("Ground")))
        {
            return Vector3.Angle(Vector3.up, hitInfo.normal);
        }
        return 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLive && !UI.inventoryActivated)
        {
            Move();
            Gravity(); // 중력
            Attack();
            Shield();
            Sit();
            Roll();
            myInfo.curSpDelay += Time.deltaTime;
            myHpBar.value = myInfo.CurHP / myInfo.TotalHP;
            if (myInfo.curSpDelay >= myInfo.SpDelay)
            {
                myInfo.CurSP += 10.0f * Time.deltaTime;
            }
        }
    }

    void Move()
    {
        bool isOnSlope = IsOnSlope();
        bool isGrounded = IsGrounded();
        bool wallCrash = WallCrash();

        desireDir.x = Input.GetAxisRaw("Horizontal");
        desireDir.z = Input.GetAxisRaw("Vertical");

        if (CalculateNextFrameGroundAngle(moveSpeed) < maxSlopeAngle)
        {
            if (isOnSlope && isGrounded) // 경사면 위에 있으면
            {
                if(!isMove)
                {
                    myRigid.constraints = RigidbodyConstraints.FreezeAll;
                }
                Vector3 dir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * desireDir;
                myRigid.velocity = AdjustDirectionToSlope(dir); // desireDir의 벡터 방향을 월드에서 카메라 기준으로 변경
                //myRigid.velocity = AdjustDirectionToSlope(desireDir);
                gravity = 0.0f;
                myRigid.useGravity = false;                
            }
            else
            {
                myRigid.constraints = RigidbodyConstraints.FreezeRotation;
                gravity = 2.5f;
                myRigid.useGravity = true;
            }
        }

        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * moveSpeed);
        curDir.z = Mathf.Lerp(curDir.z, desireDir.z, Time.deltaTime * moveSpeed);

        if(wallCrash)
        {
            AnimatorRootMotionMove();
        }

        myAnim.SetFloat("x", curDir.x);
        myAnim.SetFloat("z", curDir.z);

        float x = Mathf.Round(Mathf.Abs(curDir.x));
        float z = Mathf.Round(Mathf.Abs(curDir.z));

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


    bool PlayerStairs(Vector3 dir)
    {
        if (Physics.Raycast(myFoot.position, dir, 0.33f, LayerMask.GetMask("Wall"))
            || Physics.Raycast(myKnee.position, dir, 0.33f, LayerMask.GetMask("Wall")))
        {
            return true;
        }
        return false;
    }

    bool StairsCrash()
    {
        if (PlayerStairs(transform.right) || PlayerStairs(transform.forward) 
            || PlayerStairs(-transform.right) || PlayerStairs(-transform.forward))
        {
            return true;
        }
        return false;
    }

    bool PlayerWall(Vector3 dir)
    {
        return Physics.Raycast(transform.position, dir, 0.33f, LayerMask.GetMask("Wall"));
    }

    bool WallCrash()
    {
        if (PlayerWall(transform.right) || PlayerWall(transform.forward) || PlayerWall(-transform.right) || PlayerWall(-transform.forward))
        {
            return true;
        }
        return false;
    }

    bool IsOnSlope() // Player 바닥의 경사 확인
    {
        Ray ray = new Ray(transform.position + Vector3.up * 1.0f, Vector3.down);
        if (Physics.Raycast(ray, out slopHit, 2.0f, LayerMask.GetMask("Ground")))
        {
            var anlge = Vector3.Angle(Vector3.up, slopHit.normal);
            return anlge != 0.0f && anlge < maxSlopeAngle;
        }
        return false;
    }

    bool IsGrounded()
    {
        Vector3 boxSize = new Vector3(transform.lossyScale.x, 0.4f, transform.lossyScale.z);
        return Physics.CheckBox(groundCheck.position, boxSize, Quaternion.identity, LayerMask.GetMask("Ground"));
    }

    void Gravity() // 중력
    {
        yVelocity += gravity * Time.deltaTime;

        myRigid.AddForce(Vector3.down * yVelocity, ForceMode.VelocityChange);
    }

    void AnimatorRootMotionMove() // 벽에 충돌시 켜짐
    {
        transform.position = myAnim.rootPosition;
        transform.rotation = myAnim.rootRotation;
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
                    myInfo.CurSP -= 15.0f;
                }
            }
            myEgBar.value = myInfo.CurSP / myInfo.TotalSP;
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
        if(!(myInfo.CurSP < 20.0f))
        {
            if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    myAnim.SetTrigger("Roll");
                    myInfo.curSpDelay = 0.0f;
                    myInfo.CurSP -= 20.0f;
                }
            }
            myEgBar.value = myInfo.CurSP / myInfo.TotalSP;
        }
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapSphere(myHitPosition.position, 1.0f, EnemyMask);
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
}
