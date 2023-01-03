using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemCreate")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment, Used, ETC
    }

    public string itemName; // 아이템 이름
    public ItemType itemtype;
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab;
    public float itemAbility; // 아이템 능력
    public int itemValue; // 아이템 가격
    public int itemCount; // 아이템 개수
    public int itemMaxCount; // 아이템 최대 개수

    public string weaponType; // 무기 유형, 무기를 줍는 애니메이션 정보를 저장
}
