using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public static FileManager Inst = null;
    public static CharacterStat PlayerJsonStat;

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    public string LoadText(string filePath) // Text ���� �о����
    {
        return File.ReadAllText(filePath);
    }

    public static CharacterStat LoadJson(string filePath) // Json ���� �о����
    {
        return JsonUtility.FromJson<CharacterStat>(filePath);
    }
    public void SaveData(CharacterStat PlayerStat) // �÷��̾� ������ Json ����
    {
        string playerStatToJson = JsonUtility.ToJson(PlayerStat, true); // �� ����
        string path = Application.dataPath + "/Player.Json";
        File.WriteAllText(path, playerStatToJson);
    }

    public void GetJsonPlayerData()
    {
        string filePath = Application.dataPath + "Player.Json";
        if(File.Exists(filePath)) // ������ ���� �ϸ�
        {
            // LoadText(filePath);
            PlayerJsonStat = LoadJson(filePath);
        }
    }
}
