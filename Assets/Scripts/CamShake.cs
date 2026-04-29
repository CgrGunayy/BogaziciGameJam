using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 shakeOrigin;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			if (shakeOrigin == new Vector3(-1, -1))
				shakeOrigin = camTransform.localPosition;

			camTransform.localPosition = shakeOrigin + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
        }
		else
        {
			shakeOrigin = new Vector3(-1, -1);
        }
	}
}