using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulGageUI : MonoBehaviour
{
    public TextMeshProUGUI TMP_Text;

    // Update is called once per frame
    void Update()
    {
        TMP_Text.text = MyCharacter.Inst.playerInfo.playerStat.SoulS.ToString();
    }
}
