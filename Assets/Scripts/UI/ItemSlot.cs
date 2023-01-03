using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Item item; // 획득 아이템
    public int itemCount; // 아이템 개수
    public Image itemImage; // 아이템 이미지

    public TextMeshProUGUI text_Count;
    private ItemEffectDatabase theItemEffectDatabase;

    void Start()
    {
        theItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
    }

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
