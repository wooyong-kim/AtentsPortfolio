using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectDatabase : MonoBehaviour
{
    private const string HP = "HP", SP = "SP", MP = "MP"; // 체력, 스태미나, 마나량(미구현)

    public Player thePlayerStatus;

    public void UseItem(Item _item)
    {
        if(_item.itemtype == Item.ItemType.Used)
        {
                switch (_item.itemName)
                {
                    case HP:
                        thePlayerStatus.myInfo.CurHP += _item.itemAbility;
                        if(thePlayerStatus.myInfo.CurHP > thePlayerStatus.myInfo.TotalHP)
                        {
                            thePlayerStatus.myInfo.CurHP = thePlayerStatus.myInfo.TotalHP;
                        }
                        break;
                    case SP:
                        thePlayerStatus.myInfo.CurSP += _item.itemAbility;
                        if (thePlayerStatus.myInfo.CurSP > thePlayerStatus.myInfo.TotalHP)
                        {
                            thePlayerStatus.myInfo.CurSP = thePlayerStatus.myInfo.TotalSP;
                        }
                        break;
                }
        }
    }
}
