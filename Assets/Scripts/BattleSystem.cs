using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle // �������̽�, Ŭ������ ���ϴ� ���� ����� �����ϱ� ���� ��� 
{
    Transform transform { get; } // ��ӿ� ���� �߰� �� �ʿ���� �Լ�
    void OnDamage(float dmg);
    bool IsLive { get; }
}