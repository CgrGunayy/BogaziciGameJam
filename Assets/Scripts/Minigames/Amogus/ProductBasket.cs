using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBasket : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int positiveScore = 5;
    [SerializeField] private int negativeScore = -10;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.TryGetComponent<Product>(out Product product))
        {
            audioSource.Play();

            int score = 0;
            if (product.isNeedToRead && product.isPassed)
                score = positiveScore;
            else if (!product.isNeedToRead && product.isPassed)
                score = negativeScore;

            CashierMiniGameManager.Instance.AddProgress(score);
            Destroy(col.gameObject);
        }
    }
}
