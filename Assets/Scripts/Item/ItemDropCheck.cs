using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ItemDropCheck : MonoBehaviour
{
    public float range = 2.0f; // 아이템 습득 최대 거리
    bool DropCheck = false; // 습득 가능 체크
    RaycastHit hitInfo;
    public LayerMask ItemLayerMask;
    [SerializeField]
    TextMeshProUGUI ShowText;

    void Start()
    {

    }
    // Update is called once per frame
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
        ShowText.text = hitInfo.transform.GetComponent<ItemPickup>().item.itemName + " Get <color=yellow>" + " (E)" + "</color>";
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
            if(hitInfo.transform != null)
            {
                // StartCoroutine(CanPickupcountTime(2.0f, 5.0f));
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisAppear();
            }
        }
    }

    IEnumerator CanPickupcountTime(float delayTime1, float delayTime2)
    {
        yield return new WaitForSeconds(delayTime1);
        ShowText.gameObject.SetActive(true);
        ShowText.text = hitInfo.transform.GetComponent<ItemPickup>().item.itemName + "<color=blue>" + " Pick up." + "</color>";
        yield return new WaitForSeconds(delayTime2);
        Destroy(hitInfo.transform.gameObject);
        ItemInfoDisAppear();
    }
}
