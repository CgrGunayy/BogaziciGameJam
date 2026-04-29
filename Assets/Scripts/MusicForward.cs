using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicForward : MonoBehaviour
{
    [SerializeField] private float time;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
        audioSource.time = time;
    }
}
