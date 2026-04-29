using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteTarget : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PeriMouseFollow>() != null)
        {
            StartCoroutine(GameObject.FindObjectOfType<KiteMiniGameManager>().GameWin());
        }
    }
}
