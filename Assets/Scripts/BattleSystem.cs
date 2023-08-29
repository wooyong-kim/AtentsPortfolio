using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle // 인터페이스, 클래스가 못하는 다중 상속을 보완하기 위해 사용 
{
    Transform transform { get; } // 상속에 따로 추가 할 필요없는 함수
    void OnDamage(float dmg);
    bool IsLive { get; }
}