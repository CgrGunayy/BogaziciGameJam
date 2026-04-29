using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriMouseFollow : MonoBehaviour
{
    [SerializeField] private float forcePower;
    [SerializeField] private Vector2 offset;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private KiteMiniGameManager kiteGameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        kiteGameManager = FindObjectOfType<KiteMiniGameManager>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (kiteGameManager.Started)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(Vector2.Lerp(transform.position, mousePos + offset, Time.fixedDeltaTime * forcePower));
        }
    }
}
