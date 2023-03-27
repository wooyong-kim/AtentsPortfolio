using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingData
{
    public int diffculty;
    public float Audio;
    public int Save;
}

public class Setting : MonoBehaviour
{
    SettingData data = new SettingData();
    public Slider sound;

    int diffculty;
    int save = 0;

    public void EasyDiff()
    {
        diffculty = 0;
    }

    public void HardDiff()
    {
        diffculty = 1;
    }
    public void NewGame()
    {
        save = 0;
        SceneManager.LoadScene("ActionRPG");
    }

    public void ContinueGame()
    {
        save = 1;
        SceneManager.LoadScene("ActionRPG");
    }

    public void SaveData() // 난이도, 오디오, 게임 로드 데이터 Json 저장
    {
        data.diffculty = diffculty;
        data.Audio = sound.value;
        data.Save = save;

        string json = JsonUtility.ToJson(data, true);
        string path = Application.dataPath + "/Settings.Json";
        File.WriteAllText(path, json);
    }
}
