using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class NPCCheck : MonoBehaviour
{
    public static NPCCheck Inst = null;
    public float range = 2.0f; // NPC 대화 최대 거리
    public static bool NpcCheck = false; // 대화 가능 체크
    RaycastHit hitInfo;
    public LayerMask NPCLayerMask;
    public UI theUI;
    [SerializeField]
    TextMeshProUGUI ShowText;
    // 플레이어의 특정 시야 안에 NPC가 존재하면 Text 출력
    // Update is called once per frame
    void Update()
    {
        CheckNPC();
    }

    void CheckNPC()
    {
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hitInfo, range, NPCLayerMask))
        {
            NPCTalkAppear();
        }
        else
        {
            NPCTalkDisAppear();
        }
    }

    void NPCTalkAppear()
    {
        NpcCheck = true;
        ShowText.gameObject.SetActive(true);
        ShowText.text = "<color=yellow>(K)</color>" + " Talk";
    }

    void NPCTalkDisAppear()
    {
        NpcCheck = false;
        ShowText.gameObject.SetActive(false);
    }
}
