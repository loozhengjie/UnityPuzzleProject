using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

   public void playSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
