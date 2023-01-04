using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public void StatLevelUp(Player myStat) // FileManager의 SaveData와 같이 레벨이 올라갈때 실행
    {
        // void에서 변경 필요할수도
        myStat.myInfo.LV = FileManager.PlayerJsonStat.Vigor
            + FileManager.PlayerJsonStat.Attunement + FileManager.PlayerJsonStat.Endurance
            + FileManager.PlayerJsonStat.Vitality + FileManager.PlayerJsonStat.Strength;
        myStat.myInfo.MaxHp = 130 + FileManager.PlayerJsonStat.Vigor * 50 + FileManager.PlayerJsonStat.Vitality * 20;
        myStat.myInfo.MaxSP = 80 + FileManager.PlayerJsonStat.Endurance * 20;
        myStat.myInfo.AttackDG = 50 + FileManager.PlayerJsonStat.Vitality * 10 + FileManager.PlayerJsonStat.Strength * 20;
        myStat.myInfo.AttackDelay = 2.0f - FileManager.PlayerJsonStat.Attunement / 100;
    }
}
