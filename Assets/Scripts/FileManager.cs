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

    public string LoadText(string filePath) // Text ���� �о����
    {
        return File.ReadAllText(filePath);
    }

    public static CharacterStat LoadJson(string filePath) // Json ���� �о����
    {
        CharacterStat stat = JsonUtility.FromJson<CharacterStat>(filePath);
        return stat;
    }
    public void SaveData(CharacterStat PlayerStat) // �÷��̾� ������ Json ����
    {
        string playerStatToJson = JsonUtility.ToJson(PlayerStat, true); // �� ����
        string path = Application.dataPath + "/Player.Json";
        File.WriteAllText(path, playerStatToJson);
    }

    public void GetJsonPlayerData()
    {     
        string fileName = @"Player" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // ������ ���� �ϸ�
        {
            PlayerJsonLead = LoadText(filePath);
            MyCharacter.Inst.playerInfo.playerStat = LoadJson(PlayerJsonLead);
        }
    }

    public void GetJsonPlayerDefaultData()
    {
        string fileName = @"PlayerDefault" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // ������ ���� �ϸ�
        {
            PlayerJsonLead = LoadText(filePath);
            MyCharacter.Inst.playerInfo.playerStat = LoadJson(PlayerJsonLead);
        }
    }

    public void GetJsonEnemyEasyData()
    {
        string fileName = @"EnemyEasy" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // ������ ���� �ϸ�
        {
            EnemyJsonLead = LoadText(filePath);
            MyCharacter.Inst.enemyInfo.playerStat = LoadJson(EnemyJsonLead);
        }
    }

    public void GetJsonEnemyHardData()
    {
        string fileName = @"EnemyHard" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // ������ ���� �ϸ�
        {
            EnemyJsonLead = LoadText(filePath);
            MyCharacter.Inst.enemyInfo.playerStat = LoadJson(EnemyJsonLead);
        }
    }
}
