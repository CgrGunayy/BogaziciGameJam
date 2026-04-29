using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PlayerMovement>() != null)
        {
            GameManager.SoundManager.musicSource.clip = music;
            GameManager.SoundManager.musicSource.Play();
        }
    }
}
