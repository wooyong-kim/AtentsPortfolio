using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    Transform HeadPos { get; }
    Transform transform { get; }
    void OnDamage(float dmg);
    bool IsLive
    {
        get;
    }
}

public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
