using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class NPCCheck : MonoBehaviour
{
    public static NPCCheck Inst = null;
    public float range = 2.0f; // NPC ��ȭ �ִ� �Ÿ�
    public static bool NpcCheck = false; // ��ȭ ���� üũ
    RaycastHit hitInfo;
    public LayerMask NPCLayerMask;
    public UI theUI;
    [SerializeField]
    TextMeshProUGUI ShowText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
