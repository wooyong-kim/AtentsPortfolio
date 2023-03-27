using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        IBattle ib = other.GetComponent<IBattle>();
        ib?.OnDamage(30.0f * enemy.myInfo.AttackDG);
    }
}
