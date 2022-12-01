using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterProperty
{
    Vector2 desireDir = Vector2.zero;
    Vector2 curDir = Vector2.zero;
    public LayerMask EnemyMask = default;
    public Transform myHitPosition;
    public float moveSpeed = 10.0f;
    bool IsAir = false;
    bool IsComboable = false;
    int ClickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (!IsAir) myRigid.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Shield();
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
        if(!myAnim.GetBool("IsAttacking"))
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
        if(!myAnim.GetBool("IsAttacking"))
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

    void Roll()
    {
        if (!myAnim.GetBool("IsAttacking"))
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 dir = new Vector3(desireDir.x, 0, desireDir.y);
                myAnim.SetTrigger("Roll");
                transform.localRotation = Quaternion.Lerp(transform.localRotation, dir, 1.0f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsAir = true;
        }
    }
}
