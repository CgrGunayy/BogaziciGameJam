using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public List<AudioClip> walkSounds = new List<AudioClip>();

    private PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        StartCoroutine(WalkSoundLooper());
    }
    private IEnumerator WalkSoundLooper()
    {
        yield return new WaitForSeconds(0.48f);
        if (playerMovement.isPlayerMoving)
            GameManager.SoundManager.PlaySfx(walkSounds[Random.Range(0, walkSounds.Count)]);
        StartCoroutine(WalkSoundLooper());
    }
}
