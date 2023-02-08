using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExit : MonoBehaviour
{
    public void OnClickExit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
