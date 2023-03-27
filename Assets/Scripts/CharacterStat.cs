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
        set => Souls = value;
    }

    //�ʵ� ( Field )
    [SerializeField] float maxHp;
    [SerializeField] float curHP;
    
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
        set => vigor = value;
    }
    [SerializeField] int attunement; // ���߷�(attackDelay ����)
    public int Attunement
    {
        get => attunement;
        set => attunement = value;
    }
    [SerializeField] int endurance; // ������(maxSP ���)
    public int Endurance
    {
        get => endurance; 
        set => endurance = value;
    }

    [SerializeField] int vitality; // ü��(HP, DG �Ѵ� ���)
    public int Vitality
    {
        get => vitality;
        set => vitality = value;
    }

    [SerializeField] int strength; // �ٷ�(attakDG ���)
    public int Strength
    {
        get => strength;
        set => strength = value;
    }
}
