using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PosionSlot : MonoBehaviour
{
    public Image itemImage; // 아이템 이미지
    Image myImage;
    public TextMeshProUGUI text_Count;
    TextMeshProUGUI mytext;

    void Start()
    {
        myImage = transform.GetChild(0).GetComponent<Image>();
        mytext = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        myImage.sprite = itemImage.sprite;
        mytext.text = text_Count.text;

        if (myImage.sprite != null)
        {   
            SetColor(1);
        }
        else
        {
            SetColor(0);
        }     
    }

    void SetColor(float _alpah) // 아이템 이미지 투명도
    {
        Color color = myImage.color;
        color.a = _alpah;
        myImage.color = color;
    }
}
