using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBarName : MonoBehaviour
{
    public GameObject Enemy;
    public TextMeshProUGUI myText;
    // public Text EnemyName;
    // Start is called before the first frame update
    void Start()
    {
        myText.text = Enemy.name;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
