using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterStat
{
    [SerializeField] int Lv; // 생명력, 집중력, 지구력, 체력, 근력 합산 값
    public int LV
    {
        get => Lv;
        set => Lv = value;
    }

    [SerializeField] int Souls; // 레벨 상승 재료
    public int SoulS
    {
        get => Souls;
    }

    //필드 ( Field )
    [SerializeField] float maxHp;
    [SerializeField] float curHP;
    //프로퍼티 ( Property )
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }
    public float CurHP
    {
        get => curHP;
        set => curHP = Mathf.Clamp(value, 0.0f, maxHp);
    }
    [SerializeField] float maxSP;
    [SerializeField] float curSP;
    public float MaxSP
    {
        get => maxSP;
        set => maxSP = value;
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
        set => attackDG = value;
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
        set => attackDelay = value;
    }

    [SerializeField] float spDelay;
    public float curSpDelay;
    public float SpDelay
    {
        get => spDelay;
    }

    [SerializeField] int vigor; // 생명력(maxHP 상승)
    public int Vigor
    {
        get => vigor;
    }
    [SerializeField] int attunement; // 집중력(attackDelay 감소)
    public int Attunement
    {
        get => attunement;
    }
    [SerializeField] int endurance; // 지구력(maxSP 상승)
    public int Endurance
    {
        get => endurance;
    }

    [SerializeField] int vitality; // 체력(HP, DG 둘다 상승)
    public int Vitality
    {
        get => vitality;
    }

    [SerializeField] int strength; // 근력(attakDG 상승)
    public int Strength
    {
        get => strength;
    }

    public CharacterStat(int Lv, int Souls, float hp, float sp, float attackDG, float moveSpeed, float rotSpeed, 
        float attackDelay, float spDelay, int vigor, int attunement, int endurance, int vitality, int strength)
    {
        this.Lv = Lv;
        this.Souls = Souls;
        curSP = maxSP = sp;
        curHP = maxHp = hp;
        this.attackDG = attackDG;
        this.moveSpeed = moveSpeed;
        this.rotSpeed = rotSpeed;
        this.attackDelay = attackDelay;
        curAttackDelay = 0.0f;
        this.spDelay = spDelay;
        curSpDelay = 0.0f;
        this.vigor = vigor;
        this.attunement = attunement;
        this.endurance = endurance;
        this.vitality = vitality;
        this.strength = strength;
    }
    
}
