using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductReader : MonoBehaviour
{
    [SerializeField] private AudioClip positiveSound;
    [SerializeField] private AudioClip negativeSound;

    private Animator laserAnim;
    private AudioSource laserSfx;

    private void Awake()
    {
        laserAnim = GetComponent<Animator>();
        laserSfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<Product>(out Product product))
        {
            laserAnim.SetTrigger("Laser");
            if(!laserSfx.isPlaying)
                laserSfx.PlayOneShot(product.isNeedToRead ? positiveSound : negativeSound);

            product.isPassed = true;

            if (!product.isNeedToRead)
                CashierMiniGameManager.Instance.AddProgress(-5);
        }
    }
}
