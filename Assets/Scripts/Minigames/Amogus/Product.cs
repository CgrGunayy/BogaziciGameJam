using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public bool isClicked { get; private set; }
    public bool isFading;
    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 differanceBetweenMouse;
    private float startGravity;

    public bool isNeedToRead;
    public bool isPassed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        startGravity = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        Dragging();
    }

    private void Dragging()
    {
        if (isClicked)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            rb.position = (Vector2)CashierMiniGameManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition) - differanceBetweenMouse;
        }
    }

    private void OnMouseDown()
    {
        if (isFading)
            return;

        differanceBetweenMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.isKinematic = true;
        isClicked = true;
    }

    private void OnMouseUp()
    {
        rb.gravityScale = startGravity;
        isClicked = false;
        rb.isKinematic = false;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Conveyor")
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.left * moveSpeed;
        }
    }
}
