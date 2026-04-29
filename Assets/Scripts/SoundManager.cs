using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public void SetMute(bool mute)
    {
        musicSource.mute = mute;
        sfxSource.mute = mute;
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip,1);
    }

    public void PlayMusic(AudioClip clip)
    {
        sfxSource.clip = clip;

        if (sfxSource.isPlaying == false)
            sfxSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Stop();
    }

    public void ContinueMusic()
    {
        musicSource.Play();
    }
}
