using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // 작업모드에서 바로 적용
public class TriggerScript : MonoBehaviour
{
    public ParticleSystem PS;
    public void BreathAttack()
    {
        if (gameObject != null)
        {      
            PS.Play();
        }
        else
        {
            PS.Stop();
        }
    }

    void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> Enter = new List<ParticleSystem.Particle>();
        int numEnter = PS.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, Enter);

        for(int i = 0; i < numEnter; ++i)
        {
            IBattle ib = PS.trigger.GetCollider(0).GetComponent<IBattle>();
            ib?.OnDamage(10.0f);
        }
    }
}
