using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriController : MonoBehaviour
{
    public bool followTarget = false;
    public Vector2 offset;

    [SerializeField] private float speed;
    [Space]
    [SerializeField] private Transform target;
    [SerializeField] private int spinDelay;

    private int idleLoopAmount;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(followTarget && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)target.position + offset, speed * Time.deltaTime);
        }

        float distance = Vector2.Distance(transform.position, (Vector2)target.position + offset);
        if(distance <= 0.1f)
        {
            TrySpinAnimation();
        }
    }

    public void IncreaseIdleLoop()
    {
        idleLoopAmount += 1;
    }

    private void TrySpinAnimation()
    {
        if(idleLoopAmount >= spinDelay)
        {
            anim.SetTrigger("Spin");
            idleLoopAmount = 0;
        }
    }
}
