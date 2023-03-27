using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent Attack = null;
    public UnityEvent ComboCheckStart = null;
    public UnityEvent ComboCheckEnd = null;
    public UnityEvent AttackSound = null;
    public UnityEvent ComboAttackSound = null;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnComboCheckStart()
    {
        ComboCheckStart?.Invoke();
    }

    public void OnComboCheckEnd()
    {
        ComboCheckEnd?.Invoke();
    }

    public void OnAttackSound()
    {
        AttackSound?.Invoke();
    }

    public void OnComboAttackSound()
    {
        ComboAttackSound?.Invoke();
    }
}
