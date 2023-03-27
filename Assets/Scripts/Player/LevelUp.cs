using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public void LevelUP_Vigor()
    {
        if(MyCharacter.Inst.playerInfo.playerStat.SoulS >= 100)
        {
            MyCharacter.Inst.playerInfo.playerStat.Vigor = MyCharacter.Inst.playerInfo.playerStat.Vigor + 1;
            MyCharacter.Inst.StatLevelUp();
            MyCharacter.Inst.playerInfo.playerStat.SoulS -= 100;
            FileManager.Inst.StatChange = true;      
        }     
    }
    public void LevelUP_Attunement()
    {
        if (MyCharacter.Inst.playerInfo.playerStat.SoulS >= 100)
        {
            MyCharacter.Inst.playerInfo.playerStat.Attunement = MyCharacter.Inst.playerInfo.playerStat.Attunement + 1;
            MyCharacter.Inst.StatLevelUp();
            MyCharacter.Inst.playerInfo.playerStat.SoulS -= 100;
            FileManager.Inst.StatChange = true;         
        }            
    }
    public void LevelUP_Endurance()
    {
        if (MyCharacter.Inst.playerInfo.playerStat.SoulS >= 100)
        {
            MyCharacter.Inst.playerInfo.playerStat.Endurance = MyCharacter.Inst.playerInfo.playerStat.Endurance + 1;
            MyCharacter.Inst.StatLevelUp();
            MyCharacter.Inst.playerInfo.playerStat.SoulS -= 100;
            FileManager.Inst.StatChange = true;
        }           
    }
    public void LevelUP_Vitality()
    {
        if (MyCharacter.Inst.playerInfo.playerStat.SoulS >= 100)
        {
            MyCharacter.Inst.playerInfo.playerStat.Vitality = MyCharacter.Inst.playerInfo.playerStat.Vitality + 1;
            MyCharacter.Inst.StatLevelUp();
            MyCharacter.Inst.playerInfo.playerStat.SoulS -= 100;
            FileManager.Inst.StatChange = true;
        }            
    }
    public void LevelUP_Strength()
    {
        if (MyCharacter.Inst.playerInfo.playerStat.SoulS >= 100)
        {
            MyCharacter.Inst.playerInfo.playerStat.Strength = MyCharacter.Inst.playerInfo.playerStat.Strength + 1;
            MyCharacter.Inst.StatLevelUp();
            MyCharacter.Inst.playerInfo.playerStat.SoulS -= 100;
            FileManager.Inst.StatChange = true;
        }          
    }
}
