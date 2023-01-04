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

    public string LoadText(string filePath) // Text 파일 읽어오기
    {
        return File.ReadAllText(filePath);
    }

    public static CharacterStat LoadJson(string filePath) // Json 파일 읽어오기
    {
        return JsonUtility.FromJson<CharacterStat>(filePath);
    }
    public void SaveData(CharacterStat PlayerStat) // 플레이어 데이터 Json 저장
    {
        string playerStatToJson = JsonUtility.ToJson(PlayerStat, true); // 줄 엔터
        string path = Application.dataPath + "/Player.Json";
        File.WriteAllText(path, playerStatToJson);
    }

    public void GetJsonPlayerData()
    {
        // string filePath = ;
        if (File.Exists(Application.dataPath + "/Player.Json")) // 파일이 존재 하면
        {
            // LoadText(filePath);
            // PlayerJsonStat = LoadJson(filePath);

            string json = File.ReadAllText(Application.dataPath + "/Player.Json");
            PlayerJsonStat = JsonUtility.FromJson<CharacterStat>(json);
            Debug.Log(PlayerJsonStat.CurHP);
        }
    }
}
