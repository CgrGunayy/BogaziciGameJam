using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerHorizontalInput { get; private set; }
    [SerializeField] private float movementSpeed;

    [SerializeField] private bool canWalk = true;
    private Rigidbody2D rb;
    private Animator anim;

    public bool isPlayerMoving { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canWalk = true;
    }

    private void LateUpdate()
    {
        if (!canWalk)
            return;

        UpdateAnimation();
        UpdateFacing();
    }

    private void Update()
    {
        if (!canWalk)
        {
            isPlayerMoving = false;
            return;
        }

        PlayerHorizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(PlayerHorizontalInput * movementSpeed, rb.velocity.y);

        if (rb.velocity.magnitude > 0.11f)
            isPlayerMoving = true;
        else
            isPlayerMoving = false;
    }

    public void SetCanWalk(bool canWalk)
    {
        this.canWalk = canWalk;

        if (!canWalk)
        {
            anim.SetBool("isWalking", false);
            isPlayerMoving = false;
            rb.velocity = Vector2.zero;
        }
    }

    public void DisableMoving()
    {
        canWalk = false;
        anim.SetBool("isWalking", false);
        isPlayerMoving = false;
        rb.velocity = Vector2.zero;
    }

    private void UpdateAnimation()
    {
        anim.SetBool("isWalking", PlayerHorizontalInput != 0);
    }

    private void UpdateFacing()
    {
        if(PlayerHorizontalInput != 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, PlayerHorizontalInput == 1 ? 0 : 180, 0));
    }
}
