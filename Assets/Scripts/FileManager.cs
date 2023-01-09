using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public static FileManager Inst = null;
    public static string PlayerJsonLead;

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    public string LoadText(string filePath) // Text ���� �о����
    {
        return File.ReadAllText(filePath);
    }

    public static PlayerInfo LoadJson(string filePath) // PlayerInfo�������� Json ���� �о����
    {
        return JsonUtility.FromJson<PlayerInfo>(filePath);
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
            MyCharacter.Inst.playerInfo = LoadJson(PlayerJsonLead);
        }
    }
}
