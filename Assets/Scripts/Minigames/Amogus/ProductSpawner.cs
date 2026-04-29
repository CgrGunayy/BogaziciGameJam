using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public bool OnGenerateMod { get; private set; }

    public float spawnDelay;
    public List<Sprite> productCorrectSprites = new List<Sprite>();
    public List<Sprite> productFalseSprites = new List<Sprite>();
    public GameObject productPrefab;
    private bool canSpawn = true;

    private float spawnTimer;

    private void Update()
    {
        if (OnGenerateMod == false)
            return;

        if(spawnTimer <= 0)
        {
            if (canSpawn)
            {
                GameObject spawnedProduct = Instantiate(productPrefab, transform.position, Quaternion.identity);

                int random = Random.Range(1, 4);

                if (random == 1)
                {
                    spawnedProduct.GetComponent<Product>().isNeedToRead = false;
                    spawnedProduct.GetComponent<SpriteRenderer>().sprite = productFalseSprites[Random.Range(0, productFalseSprites.Count)];
                }
                else
                {
                    spawnedProduct.GetComponent<Product>().isNeedToRead = true;
                    spawnedProduct.GetComponent<SpriteRenderer>().sprite = productCorrectSprites[Random.Range(0, productCorrectSprites.Count)];
                }
            }

            spawnTimer = spawnDelay;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
        }

    }

    public void StartGeneration()
    {
        OnGenerateMod = true;
    }

    public void StopGeneration()
    {
        OnGenerateMod = false;
        spawnTimer = spawnDelay;
    }

    public void ClearProducts()
    {
        foreach (var productObj in GameObject.FindGameObjectsWithTag("Product"))
        {
            Destroy(productObj);
        }
    }

    public IEnumerator Restart()
    {
        ClearProducts();
        OnGenerateMod = false;
        yield return new WaitForSeconds(2f);
        OnGenerateMod = true;
    }
}
