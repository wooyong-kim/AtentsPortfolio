using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterStat
{
    //필드 ( Field )
    [SerializeField] float maxHp;
    [SerializeField] float curHP;
    //프로퍼티 ( Property )
    public float TotalHP
    {
        get => maxHp;
    }
    public float CurHP
    {
        get => curHP;
        set => curHP = Mathf.Clamp(value, 0.0f, maxHp);
    }

    [SerializeField] float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
    }

    [SerializeField] float rotSpeed;
    public float RotSpeed
    {
        get => rotSpeed;
    }
    [SerializeField] float attackDelay;
    public float curAttackDelay;
    public float AttackDelay
    {
        get => attackDelay;
    }

    public CharacterStat(float hp, float moveSpeed, float rotSpeed, float attackDelay)
    {
        curHP = maxHp = hp;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.attackDelay = attackDelay;
        curAttackDelay = 0.0f;
    }
}
