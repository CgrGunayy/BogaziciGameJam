using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Product>(out Product product))
        {
            if (product.isNeedToRead)
                CashierMiniGameManager.Instance.AddProgress(-5);
            Destroy(col.gameObject);
        }
    }
}
