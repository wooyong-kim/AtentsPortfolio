using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimEvent : MonoBehaviour
{
    public UnityEvent Punch = null;

    public void OnPunch()
    {
        Punch?.Invoke();
    }
}
