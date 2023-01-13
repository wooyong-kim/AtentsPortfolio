using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEvent : CharacterMovement
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IBattle ib = other.gameObject.GetComponent<IBattle>();

            if (rand >= 80)
            {
                ib?.OnDamage(10.0f); // PunchAttack
            }
            else if (80 > rand && rand >= 50)
            {
                ib?.OnDamage(20.0f); // SwipingAttack
            }
            else if (50 > rand && rand >= 30)
            {
                ib?.OnDamage(20.0f); // BreathAttack
            }
            else if (30 > rand && rand >= 20)
            {
                ib?.OnDamage(30.0f); // RunAttack
            }
            else
            {
                ib?.OnDamage(50.0f); // JumpAttack
            }
        }
    }
}
