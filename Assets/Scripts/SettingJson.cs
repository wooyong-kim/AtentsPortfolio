using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingJson : MonoBehaviour
{
    public static string SettingJsonLead;
    public SettingData set;

    private void OnEnable()
    {
        string fileName = @"Settings" + ".Json";
        string filePath = Application.dataPath + "/" + fileName;
        if (File.Exists(filePath)) // ������ ���� �ϸ�
        {
            SettingJsonLead = File.ReadAllText(filePath);
            set = JsonUtility.FromJson<SettingData>(SettingJsonLead);
        }
    }
}
