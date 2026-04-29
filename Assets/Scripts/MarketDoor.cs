using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketDoor : MonoBehaviour
{
    public bool isInteractable = true;

    [SerializeField] private AudioClip doorSound;
    [SerializeField] private SpriteRenderer doorRenderer;
    [SerializeField] private MinigameLoader minigameLoader;

    private PlayerInteraction playerInteraction;
    private bool isInTrigger;

    private void Start()
    {
        playerInteraction = GameManager.PlayerMovement.GetComponent<PlayerInteraction>();
    }

    private void LateUpdate()
    {
        if (isInteractable && isInTrigger && !GameManager.DialogSystem.IsDialogOnGoing && Input.GetButtonDown("InteractionButton"))
        {
            if(GameManager.DaySystem.MarketDone == false)
            {
                GameManager.SoundManager.PlaySfx(doorSound);
                minigameLoader.LoadMinigame(4);
                GameManager.DaySystem.MarketDone = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.DaySystem.CurrentDay == 3)
            {
                this.enabled = false;
                return;
            }
            isInTrigger = true;

            if (GameManager.DaySystem.MarketDone == false)
                playerInteraction.SetInteractionActive(true);

            if (doorRenderer != null)
                doorRenderer.material.SetFloat("_OutlineThickness", 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = false;
            playerInteraction.SetInteractionActive(false);
            if (doorRenderer != null)
                doorRenderer.material.SetFloat("_OutlineThickness", 0);
        }
    }
}
