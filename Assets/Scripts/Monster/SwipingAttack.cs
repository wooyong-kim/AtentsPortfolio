using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipingAttack : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        IBattle ib = other.GetComponent<IBattle>();
        ib?.OnDamage(50.0f * enemy.myInfo.AttackDG);
    }
}
