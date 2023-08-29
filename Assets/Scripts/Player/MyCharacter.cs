using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 플레이어, 몬스터 데이터 설정 
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
    public PlayerInfo enemyInfo = new PlayerInfo();

    public void NewData()
    {
        FileManager.Inst.GetJsonPlayerDefaultData();
        StatLevelUp();
        DefaultStats();
        FileManager.Inst.SaveData(playerInfo.playerStat);
    }

    public void LoadData()
    {
        FileManager.Inst.GetJsonPlayerData();  
        StatLevelUp();
        DefaultStats();
        FileManager.Inst.SaveData(playerInfo.playerStat);
    }

    public void LoadEnemyEasyData()
    {
        FileManager.Inst.GetJsonEnemyEasyData();
    }

    public void LoadEnemyHardData()
    {
        FileManager.Inst.GetJsonEnemyHardData();
    }

    void DefaultStats()
    {
        playerInfo.playerStat.CurHP = playerInfo.playerStat.MaxHp;
        playerInfo.playerStat.CurSP = playerInfo.playerStat.CurSP;
    }
    public void StatLevelUp()
    {
        playerInfo.playerStat.LV = playerInfo.playerStat.Vigor + playerInfo.playerStat.Attunement + playerInfo.playerStat.Endurance
            + playerInfo.playerStat.Vitality + playerInfo.playerStat.Strength;
        playerInfo.playerStat.MaxHp = 130 + playerInfo.playerStat.Vigor * 50 + playerInfo.playerStat.Vitality * 20;
        playerInfo.playerStat.MaxSP = 80 + playerInfo.playerStat.Endurance * 20;
        playerInfo.playerStat.AttackDG = playerInfo.playerStat.Vitality * 10 + playerInfo.playerStat.Strength * 20;
        playerInfo.playerStat.AttackDelay = 2.0f - playerInfo.playerStat.Attunement / 100;
    }
}
