using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProductStoper : MonoBehaviour
{
    [SerializeField] private float fadeAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Product>(out Product product))
        {
            Rigidbody2D rb = product.gameObject.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;

            if (!product.isClicked)
            {
                SpriteRenderer spriteRenderer = product.gameObject.GetComponent<SpriteRenderer>();
                product.isFading = true;

                if(product.isNeedToRead)
                    CashierMiniGameManager.Instance.AddProgress(-5);

                StartCoroutine(Fade(spriteRenderer));
            }
        }
    }

    private IEnumerator Fade(SpriteRenderer spriteRenderer)
    {
        float value = 0;
        float deltaTime = Time.deltaTime;
        WaitForSeconds wfs = new WaitForSeconds(deltaTime);
        
        while (value < 1)
        {
            yield return wfs;
            value += Time.deltaTime * fadeAmount * 1.5f;
            spriteRenderer?.material.SetFloat("_PixelAmount", value);
        }

        if(spriteRenderer != null)
            Destroy(spriteRenderer.gameObject);
    }
}
