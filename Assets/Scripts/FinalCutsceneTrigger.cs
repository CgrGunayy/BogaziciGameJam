using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector manzaraCutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            manzaraCutscene.Play();
    }
}
