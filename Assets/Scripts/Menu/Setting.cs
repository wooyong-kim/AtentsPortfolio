using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingData
{
    public int diffculty;
    public float Audio;
}

public class Setting : MonoBehaviour
{
    SettingData data = new SettingData();
    public Slider sound;

    int diffculty;

    public void EasyDiff()
    {
        diffculty = 0;
    }

    public void HardDiff()
    {
        diffculty = 1;
    }

    public void SaveData() // 난이도, 오디오 데이터 Json 저장
    {
        data.diffculty = diffculty;
        data.Audio = sound.value;

        string json = JsonUtility.ToJson(data, true);
        string path = Application.dataPath + "/Settings.Json";
        File.WriteAllText(path, json);
    }
}
