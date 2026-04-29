using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLamp : MonoBehaviour
{
    public float minDoAnimTime;
    public float maxDoAnimTime;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(AnimTimerLoop());
    }
    private IEnumerator AnimTimerLoop()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("Go", false);
        float randomTime = Random.Range(minDoAnimTime, maxDoAnimTime);
        yield return new WaitForSeconds(randomTime);
        anim.SetBool("Go", true);
        StartCoroutine(AnimTimerLoop());
    }
}
