using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    static FileManager _inst = null;
    public static FileManager Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = (new GameObject("FileManager")).AddComponent<FileManager>();
            }
            return _inst;
        }
    }

    public static string PlayerJsonLead;
    public static string EnemyJsonLead;
    public bool StatChange = true;

    public string LoadText(string filePath) // Text 파일 읽어오기
    {
        return File.ReadAllText(filePath);
    }

    public static CharacterStat LoadJson(string filePath) // Json 파일 읽어오기
    {
        CharacterStat stat = JsonUtility.FromJson<CharacterStat>(filePath);
        return stat;
    }
    public void SaveData(CharacterStat PlayerStat) // 플레이어 데이터 Json 저장
    {
        string playerStatToJson = JsonUtility.ToJson(PlayerStat, true); // 줄 엔터
        string path = Application.dataPath + "/Player.Json";
        File.WriteAllText(path, playerStatToJson);
    }

    public void GetJsonPlayerData()
    {     
        string fileName = @"Player" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // 파일이 존재 하면
        {
            PlayerJsonLead = LoadText(filePath);
            MyCharacter.Inst.playerInfo.playerStat = LoadJson(PlayerJsonLead);
        }
    }

    public void GetJsonPlayerDefaultData()
    {
        string fileName = @"PlayerDefault" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // 파일이 존재 하면
        {
            PlayerJsonLead = LoadText(filePath);
            MyCharacter.Inst.playerInfo.playerStat = LoadJson(PlayerJsonLead);
        }
    }

    public void GetJsonEnemyEasyData()
    {
        string fileName = @"EnemyEasy" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // 파일이 존재 하면
        {
            EnemyJsonLead = LoadText(filePath);
            MyCharacter.Inst.enemyInfo.playerStat = LoadJson(EnemyJsonLead);
        }
    }

    public void GetJsonEnemyHardData()
    {
        string fileName = @"EnemyHard" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // 파일이 존재 하면
        {
            EnemyJsonLead = LoadText(filePath);
            MyCharacter.Inst.enemyInfo.playerStat = LoadJson(EnemyJsonLead);
        }
    }
}
