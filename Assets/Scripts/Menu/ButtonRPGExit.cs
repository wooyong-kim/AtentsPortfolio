using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRPGExit : MonoBehaviour
{
    public void ExitRPGGame()
    {
        FileManager.Inst.SaveData(MyCharacter.Inst.playerInfo.playerStat); // Player.Json�� ������ ����
        SceneManager.LoadScene("Menu");
    }
}
