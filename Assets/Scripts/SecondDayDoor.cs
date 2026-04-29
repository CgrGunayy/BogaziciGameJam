using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDayDoor : MonoBehaviour
{
    private PlayerInteraction playerInteraction;
    private bool isInTrigger;

    private void LateUpdate()
    {
        if (isInTrigger && !GameManager.DialogSystem.IsDialogOnGoing && Input.GetButtonDown("InteractionButton"))
        {
            GameManager.DaySystem.SetDay(2);
            GameManager.DaySystem.cinemachineConfiner.m_BoundingShape2D = GameManager.DaySystem.thirdFloorBoundry;
            GameManager.DaySystem.SetIsOnOutside(false);
            GameManager.CamsHandler.vCam.m_Lens.OrthographicSize = 5;
            GameManager.Elevator.SetCurrentFloor(3);
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;

            if (GameManager.PlayerMovement != null)
                playerInteraction = GameObject.FindObjectOfType<PlayerInteraction>();

            playerInteraction.SetInteractionActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = false;

            if (GameManager.PlayerMovement != null)
                playerInteraction = GameObject.FindObjectOfType<PlayerInteraction>();

            playerInteraction.SetInteractionActive(false);
        }
    }
}
