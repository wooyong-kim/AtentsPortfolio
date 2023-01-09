using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI: MonoBehaviour
{
    public static UI Inst = null;
    public static bool inventoryActivated = false;

    public GameObject Ui; // UI 창
    public GameObject ItemSetting; // 슬롯들의 부모
    public GameObject StatsSetting;

    public List<Transform> slots = new List<Transform>(); // 슬롯들 배열
    public List<Transform> stats = new List<Transform>(); // 슬롯들 배열

    string[] statsText = new string[8];

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Ui.SetActive(false);

        for(int i = 0; i < ItemSetting.transform.childCount; ++i)
        {
            slots.Add(ItemSetting.transform.GetChild(i));
        }
        for (int i = 0; i < StatsSetting.transform.childCount; ++i)
        {
            stats.Add(StatsSetting.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;
            PlayerStats();

            if (inventoryActivated)
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
    
    public void PlayerStats()
    {
        StartCoroutine(PlayerStatUI());

        for(int i = 0; i < stats.Count; ++i)
        {
            stats[i].GetComponent<StatUI>().TMP_Text.text = statsText[i];
        }

    }

    public IEnumerator PlayerStatUI()
    {
        while (true)
        {
            statsText[0] = MyCharacter.Inst.playerInfo.playerStat.LV.ToString();
            statsText[1] = MyCharacter.Inst.playerInfo.playerStat.Vigor.ToString();
            statsText[2] = MyCharacter.Inst.playerInfo.playerStat.Attunement.ToString();
            statsText[3] = MyCharacter.Inst.playerInfo.playerStat.Endurance.ToString();
            statsText[4] = MyCharacter.Inst.playerInfo.playerStat.Vitality.ToString();
            statsText[5] = MyCharacter.Inst.playerInfo.playerStat.Strength.ToString();
            statsText[6] = MyCharacter.Inst.playerInfo.playerStat.MaxHp.ToString();
            statsText[7] = MyCharacter.Inst.playerInfo.playerStat.MaxSP.ToString();

            yield return null;
        }
    }
    
}
