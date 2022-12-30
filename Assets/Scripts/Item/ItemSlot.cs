using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item; // 획득 아이템
    public int itemCount; // 아이템 개수
    public Image itemImage; // 아이템 이미지

    public TextMeshProUGUI text_Count;

    void SetColor(float _alpah) // 아이템 이미지 투명도
    {
        Color color = itemImage.color;
        color.a = _alpah;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1) // 인벤토리에 새로운 아이템 추가
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        text_Count.text = itemCount.ToString();
        SetColor(1);
    }

    public void SetSlotCount(int _count) // 해당 슬롯 아이템 개수 업데이트
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if(itemCount <= 0)
        {
            ClearSlot();
        }
    }

    void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;

        SetColor(0);
        text_Count.text = "0";
    }
}
