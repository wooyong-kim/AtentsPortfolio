using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI: MonoBehaviour
{

    public static bool inventoryActivated = false;

    public GameObject Ui; // UI 창
    public GameObject GridSetting;

    public ItemSlot[] slots; // 슬롯들 배열

    // Start is called before the first frame update
    void Start()
    {
        // slots = GridSetting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
