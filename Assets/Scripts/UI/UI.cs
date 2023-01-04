using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI: MonoBehaviour
{
    public static UI Inst = null;
    public static bool inventoryActivated = false;

    public GameObject Ui; // UI â
    public GameObject ItemSetting; // ���Ե��� �θ�
    public GameObject StatsSetting;

    public List<Transform> slots = new List<Transform>(); // ���Ե� �迭
    public List<Transform> stats = new List<Transform>(); // ���Ե� �迭

    string[] statsText;

    private void Awake()
    {
        if (Inst != null) Destroy(gameObject);
        Inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
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
            for (int i = 0; i < slots.Count; ++i) // �ߺ� ������ Ȯ��
            {
                if (slots[i].GetComponentInChildren<ItemSlot>().item != null)
                {
                    if (slots[i].GetComponentInChildren<ItemSlot>().item.itemName == _item.itemName)
                    {
                        // ���� �̸��� ������ +_count
                        slots[i].GetComponentInChildren<ItemSlot>().SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Count; ++i) // �ߺ� �������� ���� ��
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
            statsText[0] = FileManager.PlayerJsonStat.LV.ToString();
            statsText[1] = FileManager.PlayerJsonStat.Vigor.ToString();
            statsText[2] = FileManager.PlayerJsonStat.Attunement.ToString();
            statsText[3] = FileManager.PlayerJsonStat.Endurance.ToString();
            statsText[4] = FileManager.PlayerJsonStat.Vitality.ToString();
            statsText[5] = FileManager.PlayerJsonStat.Strength.ToString();
            statsText[6] = FileManager.PlayerJsonStat.MaxHp.ToString();
            statsText[7] = FileManager.PlayerJsonStat.MaxSP.ToString();

            yield return null;
        }
    }
}
