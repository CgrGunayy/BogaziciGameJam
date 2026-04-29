using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteCamera : MonoBehaviour
{
    public float speed;
    [SerializeField] private float increaseAmount;
    [SerializeField] private float maxY;
    public CamShake CamShaker { get; private set; }

    private void Awake()
    {
        CamShaker = GetComponent<CamShake>();
    }

    private void LateUpdate()
    {
        speed += increaseAmount;
    }

    public void ElevateCamera()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 203, maxY), -10);
    }
}
