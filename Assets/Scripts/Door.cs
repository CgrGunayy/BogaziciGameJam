using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    public bool isInteractable = true;
    
    [SerializeField] private Vector2 teleportPos;
    [SerializeField] private bool goesToOutside;
    [SerializeField] private bool goesToInside;
    [SerializeField] private bool automaticInteract;
    [SerializeField] private Camera roomCam;

    [SerializeField] private SpriteRenderer doorRenderer;
    private PlayerInteraction playerInteraction;

    private CamSettingsUpdater camBorderChanger;

    private bool isInTrigger;

    private void Start()
    {
        playerInteraction = GameManager.PlayerMovement.GetComponent<PlayerInteraction>();
        camBorderChanger = GetComponent<CamSettingsUpdater>();
    }

    private void LateUpdate()
    {
        if (isInteractable && isInTrigger && !GameManager.DialogSystem.IsDialogOnGoing && ( Input.GetButtonDown("InteractionButton") || automaticInteract))
        {
            GameManager.CamsHandler.TransitionTo(roomCam, true, TeleportPlayer);
        }
    }

    private void TeleportPlayer()
    {
        if (camBorderChanger != null)
            camBorderChanger.ApplySettings();

        if (goesToOutside)
            GameManager.DaySystem.SetIsOnOutside(true);
        else if (goesToInside)
            GameManager.DaySystem.SetIsOnOutside(false);

        GameManager.PlayerMovement.transform.position = teleportPos;
        GameManager.Peri.transform.position = teleportPos + GameManager.Peri.offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;
            playerInteraction.SetInteractionActive(!automaticInteract ? true : false);

            if(doorRenderer != null)
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