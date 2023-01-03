using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Item item; // ȹ�� ������
    public int itemCount; // ������ ����
    public Image itemImage; // ������ �̹���

    public TextMeshProUGUI text_Count;
    private ItemEffectDatabase theItemEffectDatabase;

    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
    }

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
            text_Count.text = "";
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
        text_Count.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                theItemEffectDatabase.UseItem(item);
                if(item.itemtype == Item.ItemType.Used)
                {
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.Inst.dragSlot = this;
            DragSlot.Inst.DragSetImage(itemImage);
            DragSlot.Inst.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.Inst.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Inst.SetColor(0);
        DragSlot.Inst.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.Inst.dragSlot != null)
        {
            ChangeSlot();
        }
    }

    void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.Inst.dragSlot.item, DragSlot.Inst.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.Inst.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.Inst.dragSlot.ClearSlot();
        }
    }
}
