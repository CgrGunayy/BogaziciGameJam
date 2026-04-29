using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogStarter : MonoBehaviour
{
    public bool IsInTrigger { get; private set; }

    public bool isTalkable;

    public int dialogIndex;
    public bool automaticTalk;

    private PlayerInteraction playerInteraction;

    private void Start()
    {
        playerInteraction = GameManager.PlayerMovement.GetComponent<PlayerInteraction>();
    }

    private void LateUpdate()
    {
        if (isTalkable && IsInTrigger && !GameManager.DialogSystem.IsDialogOnGoing && (Input.GetButtonDown("InteractionButton") || automaticTalk))
        {
            if (GameManager.DialogSystem.CanStartDialog(dialogIndex))
            {
                GameManager.DialogSystem.StartDialog(dialogIndex);
                playerInteraction.SetInteractionActive(false);
            }
        }
    }

    public void SetDialogIndex(int index)
    {
        dialogIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsInTrigger = true;
            if(GameManager.DialogSystem.CanStartDialog(dialogIndex))
                playerInteraction.SetInteractionActive(!automaticTalk ? true : false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsInTrigger = false;
            playerInteraction.SetInteractionActive(false);
        }
    }
}
