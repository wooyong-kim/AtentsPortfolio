using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public bool Battle = false;
    public AudioClip[] BGMSound;

    public AudioSource _speaker = null;
    public AudioSource BGMSpeeker
    {
        get
        {
            if (_speaker == null)
            {
                _speaker = GetComponent<AudioSource>();
            }
            return _speaker;
        }
    }
}
