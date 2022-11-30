using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent Function1 = null;
    public UnityEvent Attack = null;
    public UnityEvent ComboCheckStart = null;
    public UnityEvent ComboCheckEnd = null;
    public Transform leftFoot;
    public Transform rightFoot;
    public GameObject orgFootDustEffect;
    public void LeftFootDust()
    {
        Instantiate(orgFootDustEffect, leftFoot.position, leftFoot.rotation);
    }

    public void RightFootDust()
    {
        Instantiate(orgFootDustEffect, rightFoot.position, rightFoot.rotation);
    }

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnFunction1()
    {
        Function1?.Invoke();
    }

    public void OnComboCheckStart()
    {
        ComboCheckStart?.Invoke();
    }

    public void OnComboCheckEnd()
    {
        ComboCheckEnd?.Invoke();
    }
}
