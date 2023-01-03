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

    public string itemName; // ������ �̸�
    public ItemType itemtype;
    public Sprite itemImage; // ������ �̹���
    public GameObject itemPrefab;
    public float itemAbility; // ������ �ɷ�
    public int itemValue; // ������ ����
    public int itemCount; // ������ ����
    public int itemMaxCount; // ������ �ִ� ����

    public string weaponType; // ���� ����, ���⸦ �ݴ� �ִϸ��̼� ������ ����
}
