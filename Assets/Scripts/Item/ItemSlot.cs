using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour//, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Item item; // ȹ�� ������
    public int itemCount; // ������ ����
    public Image itemImage; // ������ �̹���

    public TextMeshProUGUI text_Count;

    void SetColor(float _alpah) // ������ �̹��� ����
    {
        Color color = itemImage.color;
        color.a = _alpah;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1) // �κ��丮�� ���ο� ������ �߰�
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemtype != Item.ItemType.Equipment)
        {
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
        }
        
        SetColor(1);
    }

    public void SetSlotCount(int _count) // �ش� ���� ������ ���� ������Ʈ
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
