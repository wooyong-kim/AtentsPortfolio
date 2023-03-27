using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAnimEvent : MonoBehaviour
{
    public UnityEvent PunchAttackSound = null;
    public UnityEvent SwipingAttackSound = null;
    public UnityEvent BreathAttackSound = null;
    public UnityEvent JumpAttackStartSound = null;
    public UnityEvent JumpAttackEndSound = null;

    public void OnPunchAttackSound()
    {
        PunchAttackSound?.Invoke();
    }

    public void OnSwipingAttackStartSound()
    {
        SwipingAttackSound?.Invoke();
    }

    public void OnBreathAttackSound()
    {
        BreathAttackSound?.Invoke();
    }

    public void OnJumpAttackStartSound()
    {
        JumpAttackStartSound?.Invoke();
    }

    public void OnJumpAttackEndSound()
    {
        JumpAttackEndSound?.Invoke();
    }
}
