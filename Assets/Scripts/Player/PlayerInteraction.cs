using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer interactionButtonVisual;

    private void Start()
    {
        interactionButtonVisual.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (interactionButtonVisual.gameObject.activeInHierarchy)
        {
            bool flip = GameManager.PlayerMovement.PlayerHorizontalInput == -1 ? true : GameManager.PlayerMovement.PlayerHorizontalInput == 1 ? false : interactionButtonVisual.flipX;
            interactionButtonVisual.flipX = flip;
        }
    }

    public void SetInteractionActive(bool active)
    {
        interactionButtonVisual.gameObject.SetActive(active);
    }
}
