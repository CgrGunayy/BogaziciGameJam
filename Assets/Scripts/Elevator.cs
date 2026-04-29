using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private TextMesh floorIndicator;
    [SerializeField] private Transform movingBg;
    [SerializeField] private AudioClip elevatorSound;
    [SerializeField] private AudioClip notValidSound;
    [SerializeField] private AudioClip dingSound;
    [SerializeField] private Camera corridorCam;
    [SerializeField] private CinemachineConfiner cinemachineConfiner;
    [SerializeField] private PolygonCollider2D[] floorCamBoundries;
    [SerializeField] private Vector2[] exits;
    [SerializeField] private float elevationTime;
    [SerializeField] private float elevationSpeed;

    private int currentFloorIndex;
    private bool canExit;
    private WaitForSeconds elevationDelayer;
    private PlayerInteraction playerInteraction;
    private bool isInTrigger;

    private void Start()
    {
        currentFloorIndex = 0;
        elevationDelayer = new WaitForSeconds(elevationTime);
        playerInteraction = GameManager.PlayerMovement.GetComponent<PlayerInteraction>();
    }

    private void LateUpdate()
    {
        if (isInTrigger && canExit && Input.GetButtonDown("InteractionButton"))
        {
            cinemachineConfiner.m_BoundingShape2D = floorCamBoundries[currentFloorIndex];
            GameManager.CamsHandler.TransitionTo(corridorCam, true, TeleportToExit);
        }
    }

    private void TeleportToExit()
    {
        GameManager.PlayerMovement.transform.position = exits[currentFloorIndex];
        GameManager.Peri.transform.position = (Vector2)GameManager.PlayerMovement.transform.position + GameManager.Peri.offset;
        playerInteraction.SetInteractionActive(false);
    }

    public void OnUpButton()
    {
        StartCoroutine(Elevate(-1));
    }

    public void OnDownButton()
    {
        StartCoroutine(Elevate(1));
    }

    public void ResetUsage()
    {
        upButton.interactable = true;
        downButton.interactable = true;
        playerInteraction.SetInteractionActive(true);
        canExit = true;
        floorIndicator.text = (exits.Length - currentFloorIndex).ToString();
    }

    public void SetUsageActive(bool active)
    {
        upButton.interactable = active;
        downButton.interactable = active;
        playerInteraction.SetInteractionActive(active);
        canExit = active;
    }

    public void SetCurrentFloor(int floorNum)
    {
        currentFloorIndex = (exits.Length - floorNum);
        movingBg.localPosition = new Vector2(movingBg.localPosition.x, -17.5f + 17.5f * currentFloorIndex);
    }

    private IEnumerator Elevate(int modifier)
    {
        int newFloor = currentFloorIndex + modifier;
        if (newFloor >= 0 && newFloor < exits.Length)
        {
            SetUsageActive(false);
            StartCoroutine(BackgroundMoveAnim(newFloor));
            GameManager.SoundManager.PlaySfx(elevatorSound);

            yield return elevationDelayer;
            SetUsageActive(true);

            GameManager.SoundManager.PlaySfx(dingSound);

            floorIndicator.text = (exits.Length - newFloor).ToString();
            currentFloorIndex = newFloor;
        }
        else
        {
            GameManager.SoundManager.PlaySfx(notValidSound);
        }
    }

    private IEnumerator BackgroundMoveAnim(int floorIndex)
    {
        float targetPos = -17.5f + 17.5f * floorIndex;
        float distance = Mathf.Abs(targetPos - movingBg.localPosition.y);
        float deltaTime = Time.deltaTime;

        WaitForSeconds deltaDelay = new WaitForSeconds(deltaTime);

        while (distance > 0.1f)
        {
            movingBg.localPosition = new Vector2(movingBg.localPosition.x, Mathf.Lerp(movingBg.localPosition.y, targetPos, deltaTime * elevationSpeed));
            yield return deltaDelay;
            distance = Mathf.Abs(targetPos - movingBg.localPosition.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = true;
            GameManager.SoundManager.PauseMusic();
            ResetUsage();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInTrigger = false;
            GameManager.SoundManager.ContinueMusic();
            ResetUsage();
        }
    }
}
