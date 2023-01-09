using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    public static MyCharacter Inst = null;
    public PlayerInfo playerInfo;

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    void Start()
    {
        
    }

    public void StatLevelUp()
    {
        playerInfo.playerStat.LV = playerInfo.playerStat.Vigor + playerInfo.playerStat.Attunement + playerInfo.playerStat.Endurance
            + playerInfo.playerStat.Vitality + playerInfo.playerStat.Endurance + playerInfo.playerStat.Vitality + playerInfo.playerStat.Strength;
        playerInfo.playerStat.MaxHp = 130 + playerInfo.playerStat.Vigor * 50 + playerInfo.playerStat.Vitality * 20;
        playerInfo.playerStat.MaxSP = 80 + playerInfo.playerStat.Endurance * 20;
        playerInfo.playerStat.AttackDG = 50 + playerInfo.playerStat.Vitality * 10 + playerInfo.playerStat.Strength * 20;
        playerInfo.playerStat.AttackDelay = 2.0f - playerInfo.playerStat.Attunement / 100;
    }
}
