using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacter : MonoBehaviour
{
    static MyCharacter _inst = null;
    public static MyCharacter Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = (new GameObject("MyCharacter")).AddComponent<MyCharacter>();
            }
            return _inst;
        }
    }
    public PlayerInfo playerInfo = new PlayerInfo();

    public void LoadData()
    {
        FileManager.Inst.GetJsonPlayerData();  
        StatLevelUp();
        DefaultStats();
        FileManager.Inst.SaveData(playerInfo.playerStat);
    }

    void DefaultStats()
    {
        playerInfo.playerStat.SoulS = 500;
        playerInfo.playerStat.CurHP = playerInfo.playerStat.MaxHp;
        playerInfo.playerStat.CurSP = playerInfo.playerStat.CurSP;
    }

    public void StatLevelUp()
    {
        playerInfo.playerStat.LV = playerInfo.playerStat.Vigor + playerInfo.playerStat.Attunement + playerInfo.playerStat.Endurance
            + playerInfo.playerStat.Vitality + playerInfo.playerStat.Strength;
        playerInfo.playerStat.MaxHp = 130 + playerInfo.playerStat.Vigor * 50 + playerInfo.playerStat.Vitality * 20;
        playerInfo.playerStat.MaxSP = 80 + playerInfo.playerStat.Endurance * 20;
        playerInfo.playerStat.AttackDG = 50 + playerInfo.playerStat.Vitality * 10 + playerInfo.playerStat.Strength * 20;
        playerInfo.playerStat.AttackDelay = 2.0f - playerInfo.playerStat.Attunement / 100;
    }
}
