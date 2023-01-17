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

            if (AttackNum == 0)
            {
                ib?.OnDamage(10.0f); // PunchAttack
            }
            else if (AttackNum == 1)
            {
                ib?.OnDamage(20.0f); // SwipingAttack
            }
            else if (AttackNum == 2)
            {
                // BreathAttack
            }
            else if (AttackNum == 3)
            {
                ib?.OnDamage(30.0f); // RunAttack
            }
            else // AttackNum = 4;
            {
                ib?.OnDamage(50.0f); // JumpAttack
            }
        }
    }
}
