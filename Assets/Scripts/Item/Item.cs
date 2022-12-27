using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ItemCreate")]
public class Item : ScriptableObject
{
    public string itemName; // ������ �̸�
    public Sprite itemImage; // ������ �̹���
    public float itemAbility; // ������ �ɷ�
    public int itemValue; // ������ ����
    public int itemCount; // ������ ����
    public int itemMaxCount; // ������ �ִ� ����
}
