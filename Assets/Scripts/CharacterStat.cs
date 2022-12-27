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
    [SerializeField] float maxEG;
    [SerializeField] float curEG;
    public float TotalEG
    {
        get => maxEG;
    }
    public float CurEG
    {
        get => curEG;
        set => curEG = Mathf.Clamp(value, 0.0f, maxEG);
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

    [SerializeField] float egDelay;
    public float curEgDelay;
    public float EgDelay
    {
        get => egDelay;
    }

    public CharacterStat(float hp, float eg, float moveSpeed, float rotSpeed, float attackDelay, float egDelay)
    {
        curEG = maxEG = eg;
        curHP = maxHp = hp;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.attackDelay = attackDelay;
        curAttackDelay = 0.0f;
        this.egDelay = egDelay;
        curEgDelay = 0.0f;
    }
}
