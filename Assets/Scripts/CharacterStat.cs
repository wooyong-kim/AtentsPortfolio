using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterStat
{
    [SerializeField] int Lv; // �����, ���߷�, ������, ü��, �ٷ� �ջ� ��
    public int LV
    {
        get => Lv;
        set => Lv = value;
    }

    [SerializeField] int Souls; // ���� ��� ���
    public int SoulS
    {
        get => Souls;
    }

    //�ʵ� ( Field )
    [SerializeField] float maxHp;
    [SerializeField] float curHP;
    //������Ƽ ( Property )
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

    [SerializeField] int vigor; // �����(maxHP ���)
    public int Vigor
    {
        get => vigor;
    }
    [SerializeField] int attunement; // ���߷�(attackDelay ����)
    public int Attunement
    {
        get => attunement;
    }
    [SerializeField] int endurance; // ������(maxSP ���)
    public int Endurance
    {
        get => endurance;
    }

    [SerializeField] int vitality; // ü��(HP, DG �Ѵ� ���)
    public int Vitality
    {
        get => vitality;
    }

    [SerializeField] int strength; // �ٷ�(attakDG ���)
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
