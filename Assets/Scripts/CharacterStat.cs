using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterStat
{
    [SerializeField] int Lv;
    public int LV
    {
        get => Lv;
    }
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
    [SerializeField] float maxSP;
    [SerializeField] float curSP;
    public float TotalSP
    {
        get => maxSP;
    }
    public float CurSP
    {
        get => curSP;
        set => curSP = Mathf.Clamp(value, 0.0f, maxSP);
    }

    [SerializeField] float attackDG;
    public float AttackDG
    {
        get => attackDG;
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

    [SerializeField] float spDelay;
    public float curSpDelay;
    public float SpDelay
    {
        get => spDelay;
    }

    public CharacterStat(int Lv, float hp, float sp, float attackDG, float moveSpeed, float rotSpeed, float attackDelay, float spDelay)
    {
        this.Lv = Lv;
        curSP = maxSP = sp;
        curHP = maxHp = hp;
        this.attackDG = attackDG;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.attackDelay = attackDelay;
        curAttackDelay = 0.0f;
        this.spDelay = spDelay;
        curSpDelay = 0.0f;
    }
}
