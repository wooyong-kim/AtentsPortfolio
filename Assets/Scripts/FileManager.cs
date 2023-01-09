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

    public string LoadText(string filePath) // Text 파일 읽어오기
    {
        return File.ReadAllText(filePath);
    }

    public static PlayerInfo LoadJson(string filePath) // PlayerInfo형식으로 Json 파일 읽어오기
    {
        return JsonUtility.FromJson<PlayerInfo>(filePath);
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
            MyCharacter.Inst.playerInfo = LoadJson(PlayerJsonLead);
        }
    }
}
