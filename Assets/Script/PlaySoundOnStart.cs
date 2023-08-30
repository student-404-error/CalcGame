using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    public void makeSound()
    {
        SoundManager.Instance.PlaySound(_clip);
    }
}
