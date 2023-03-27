using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{
    public Slider audioSlider;
    private void OnEnable()
    {
        audioSlider.value = transform.GetComponent<SettingJson>().set.Audio;
    }
}
