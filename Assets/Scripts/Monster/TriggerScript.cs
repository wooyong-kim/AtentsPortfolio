using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // 작업모드에서 바로 적용
public class TriggerScript : MonoBehaviour
{
    void OnParticleTrigger()
    {
        ParticleSystem PS = GetComponent();
        List inside = new List();
        int numEnter = PS.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

        for(int i = 0; i < numEnter; ++i)
        {
            ParticleSystem.Particle p = enter[i];
            
        }
    }
}
