using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    public AudioSource audiButton;
    public AudioClip clips;
    public void ButtonEffect()
    {
        audiButton.PlayOneShot(clips);
    }
}
