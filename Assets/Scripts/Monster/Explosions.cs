using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour
{
    public ParticleSystem PS;
    public void JumpAttack()
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
}
