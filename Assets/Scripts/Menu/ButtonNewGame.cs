using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNewGame : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("ActionRPG");
    }
}
