using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public TextMeshProUGUI statNum;

    public void StatNum(float _num)
    {
        float num = _num;
        statNum.text = num.ToString();
    }
}
