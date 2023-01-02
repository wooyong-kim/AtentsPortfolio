using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI: MonoBehaviour
{
    public static UI Inst = null;
    public static bool inventoryActivated = false;

    public GameObject Ui; // UI 창
    public GameObject GridSetting; // 슬롯들의 부모

    public List<Transform> slots = new List<Transform>(); // 슬롯들 배열
    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < GridSetting.transform.childCount; ++i)
        {
            slots.Add(GridSetting.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // slot[i].GetComponentInChild<ItemSlot>()S
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if(inventoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    void OpenInventory()
    {
        Ui.SetActive(true);
    }

    void CloseInventory()
    {
        Ui.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if(Item.ItemType.Equipment != _item.itemtype)
        {
            for (int i = 0; i < slots.Count; ++i) // 중복 아이템 확인
            {
                if (slots[i].GetComponentInChildren<ItemSlot>().item != null)
                {
                    if (slots[i].GetComponentInChildren<ItemSlot>().item.itemName == _item.itemName)
                    {
                        // 같은 이름이 있으면 +_count
                        slots[i].GetComponentInChildren<ItemSlot>().SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Count; ++i) // 중복 아이템이 없을 때
        {
            if (slots[i].GetComponentInChildren<ItemSlot>().item == null)
            {
                slots[i].GetComponentInChildren<ItemSlot>().AddItem(_item, _count);
                return;
            }
        }
    }
}
