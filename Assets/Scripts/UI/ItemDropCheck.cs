using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropCheck : MonoBehaviour
{
    public float range = 2.0f; // ������ ���� �ִ� �Ÿ�
    bool DropCheck = false; // ���� ���� üũ
    RaycastHit hitInfo;
    public LayerMask ItemLayerMask;
    public UI theUI;
    [SerializeField]
    TextMeshProUGUI ShowText;

    void Update()
    {
        CheckItem();
        TryDrop();  
    }

    void TryDrop()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickup();
        }
    }

    void CheckItem()
    {
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hitInfo, range, ItemLayerMask))
        {
            ItemInfoAppear();
        }
        else
        {
            ItemInfoDisAppear();
        }
    }

    void ItemInfoAppear()
    {
        DropCheck = true;
        ShowText.gameObject.SetActive(true);
        ShowText.text = hitInfo.transform.parent.transform.GetComponent<ItemPickup>().item.itemName + " Get <color=yellow>" + " (E)" + "</color>";
        
    }

    void ItemInfoDisAppear()
    {
        DropCheck = false;
        ShowText.gameObject.SetActive(false);
    }

    void CanPickup()
    {
        if(DropCheck)
        {
            if(hitInfo.transform != null) // ���� ����
            {
                Destroy(hitInfo.transform.parent.transform.gameObject);
                theUI.AcquireItem
                    (hitInfo.transform.parent.transform.GetComponent<ItemPickup>().item);
                ItemInfoDisAppear();
            }
        }
    }
}
