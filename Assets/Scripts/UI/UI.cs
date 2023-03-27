using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI: MonoBehaviour
{
    public static UI Inst = null;
    public static bool inventoryActivatedInven = false;
    public static bool inventoryActivatedOption = false;
    public static bool levelActivate = false;

    public GameObject Inven; // Inven 창
    public GameObject Option; // Option 창
    public GameObject Level;

    public GameObject ItemSetting; // 슬롯들의 부모
    public GameObject StatsSetting;
    public GameObject LevelSetting;

    public List<Transform> slots = new List<Transform>(); // slots
    public List<Transform> stats = new List<Transform>(); // stats
    public List<Transform> levelstats = new List<Transform>(); // levelstats

    string[] statsText = new string[8];

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Inven.SetActive(false);
        Option.SetActive(false);
        Level.SetActive(false);

        for (int i = 0; i < ItemSetting.transform.childCount; ++i)
        {
            slots.Add(ItemSetting.transform.GetChild(i));
        }
        for (int i = 0; i < StatsSetting.transform.childCount; ++i)
        {
            stats.Add(StatsSetting.transform.GetChild(i));
        }
        for (int i = 0; i < LevelSetting.transform.childCount; ++i)
        {
            levelstats.Add(LevelSetting.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();

        if(FileManager.Inst.StatChange) // 레벨 증가시 플레이어 스텟 UI 변경
        {
            PlayerLevelStats();           
        }
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I) && !inventoryActivatedOption && !levelActivate) // Inven
        {
            inventoryActivatedInven = !inventoryActivatedInven;
            PlayerStats();

            if (inventoryActivatedInven)
            {
                OpenInventory(); // SetActive(true);
            }
            else
            {
                CloseInventory(); // SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !inventoryActivatedInven && !levelActivate) // Option
        {
            inventoryActivatedOption = !inventoryActivatedOption;

            if (inventoryActivatedOption)
            {
                OpenOption(); // SetActive(true);
            }
            else
            {
                CloseOption(); // SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && !inventoryActivatedOption && !inventoryActivatedInven && NPCCheck.NpcCheck) // NPC
        {
            levelActivate = !levelActivate;
            PlayerLevelStats();

            if (levelActivate)
            {
                OpenLevel(); // SetActive(true);
            }
            else
            {
                CloseLevel(); // SetActive(false);
            }
        }
    }

    void OpenInventory()
    {
        Inven.SetActive(true);
    }

    void CloseInventory()
    {
        Inven.SetActive(false);
    }

    void OpenOption()
    {
        Option.SetActive(true);
    }

    void CloseOption()
    {
        Option.SetActive(false);
    }

    public void OpenLevel()
    {
        Level.SetActive(true);
    }

    public void CloseLevel()
    {
        Level.SetActive(false);
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
    public void PlayerLevelStats()
    {
        StartCoroutine(PlayerStatUI());

        for (int i = 0; i < levelstats.Count; ++i)
        {
            levelstats[i].GetComponent<StatUI>().TMP_Text.text = statsText[i];
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
