using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProperty, IBattle
{
    Vector2 desireDir = Vector2.zero;
    Vector2 curDir = Vector2.zero;

    public LayerMask EnemyMask = default;
    public CharacterStat myInfo;
    public Transform myHitPosition;
    public float moveSpeed = 10.0f;
    bool IsComboable = false;
    int ClickCount = 0;

    public void OnDamage(float dmg)
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
        myRigid.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Shield();
        Sit();
        Roll();
    }

    void Move()
    {
        desireDir.x = Input.GetAxisRaw("Horizontal");
        desireDir.y = Input.GetAxisRaw("Vertical");

        curDir.x = Mathf.Lerp(curDir.x, desireDir.x, Time.deltaTime * moveSpeed);
        curDir.y = Mathf.Lerp(curDir.y, desireDir.y, Time.deltaTime * moveSpeed);

        myAnim.SetFloat("x", curDir.x);
        myAnim.SetFloat("y", curDir.y);
    }

    void Attack()
    {
        if(!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                myAnim.SetTrigger("ComboAttack");
            }
        }

        if (IsComboable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickCount++;
            }
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
                    myAnim.SetBool("Sit", true);
                else
                    myAnim.SetBool("Sit", false);
            }
        }
    }

    void Roll()
    {
        if (!myAnim.GetBool("IsAttacking") && !myAnim.GetBool("IsRoll"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 dir = new Vector3(0 , desireDir.y, 0);
                myAnim.SetTrigger("Roll");
            }
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
}
