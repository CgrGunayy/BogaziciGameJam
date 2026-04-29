using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DaySystem : MonoBehaviour
{
    public int CurrentDay { get; private set; }
    public bool isOnApartment { get; set; }
    public SecondDayDoor SecondDayDoor { get; private set; }

    [SerializeField] private bool DEBUG_SKIPCUTSCENES;
    [SerializeField, Range(1, 3)] private int DEBUG_STARTDAY;
    [Space(20)]
    [SerializeField] private GameObject dayOneObjects;
    [SerializeField] private GameObject dayTwoObjects;
    [SerializeField] private GameObject dayThreeObjects;
    [Space(20)]
    [SerializeField] private Camera kargaRoomCam;
    [Space(20)]
    [SerializeField] private Vector2 kargoRoomPos;
    [Space(20)]
    [SerializeField] private PlayableDirector day01Cutscene;
    [SerializeField] private PlayableDirector day02Cutscene;
    public PlayableDirector day02YıldırımCutscene;
    [SerializeField] private PlayableDirector day03Cutscene;
    [SerializeField] private PlayableDirector finalCutscene;
    [Space(20)]
    [SerializeField] private GameObject day02beforeMarket;
    [SerializeField] private GameObject day02afterMarket;
    [Space(20)]
    public CinemachineConfiner cinemachineConfiner;
    public PolygonCollider2D thirdFloorBoundry;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject finalCutsceneTrigger;
    [Header("Sesler")]
    [SerializeField] private AudioClip day1Music;
    [SerializeField] private AudioClip day2MusicApartment;
    [SerializeField] private AudioClip day2MusicOutside;
    [SerializeField] private AudioClip day3Music;

    public bool MarketDone { get; set; }

    private void Awake()
    {
        SecondDayDoor = GameObject.FindGameObjectWithTag("ApartmentEnterance").GetComponent<SecondDayDoor>();
        SecondDayDoor.enabled = false;
    }

    private void Start()
    {
        rain.SetActive(false);
        SetDay(DEBUG_STARTDAY);
    }

    public void SetDay(int day)
    {
        CurrentDay = day;
        MarketDone = false;

        switch (CurrentDay)
        {
            case 1:
                GameManager.SoundManager.musicSource.clip = day1Music;
                dayOneObjects.SetActive(true);
                dayTwoObjects.SetActive(false);
                dayThreeObjects.SetActive(false);

                finalCutsceneTrigger.SetActive(false);
                GameManager.Elevator.SetCurrentFloor(3);
                GameManager.CamsHandler.vCam.m_Lens.OrthographicSize = 5;
                GameManager.CamsHandler.TransitionTo(kargaRoomCam, false, TeleportPlayerToKargaRoom);

                if(!DEBUG_SKIPCUTSCENES)
                    day01Cutscene.Play();

                SecondDayDoor.enabled = false;
                SecondDayDoor.GetComponent<Door>().enabled = true;
                break;
            case 2:
                SetIsOnOutside(false);

                dayOneObjects.SetActive(false);
                dayTwoObjects.SetActive(true);
                dayThreeObjects.SetActive(false);

                day02beforeMarket.SetActive(true);
                day02afterMarket.SetActive(false);

                finalCutsceneTrigger.SetActive(false);
                GameManager.Elevator.SetCurrentFloor(3);
                GameManager.CamsHandler.vCam.m_Lens.OrthographicSize = 5;
                GameManager.CamsHandler.TransitionTo(kargaRoomCam, true, TeleportPlayerToKargaRoom);

                if (!DEBUG_SKIPCUTSCENES)
                    day02Cutscene.Play();
                break;
            case 3:
                GameManager.SoundManager.musicSource.clip = day3Music;
                dayOneObjects.SetActive(false);
                dayTwoObjects.SetActive(false);
                dayThreeObjects.SetActive(true);

                finalCutsceneTrigger.SetActive(false);
                GameManager.Elevator.SetCurrentFloor(3);
                GameManager.CamsHandler.vCam.m_Lens.OrthographicSize = 5;
                GameManager.CamsHandler.TransitionTo(kargaRoomCam, false, TeleportPlayerToKargaRoom);

                if (!DEBUG_SKIPCUTSCENES)
                    day03Cutscene.Play();
                break;
        }
        GameManager.SoundManager.musicSource.Play();
    }

    public void SetIsOnOutside(bool isOut)
    {
        isOnApartment = !isOut;
        rain.SetActive(isOut);

        if (GameManager.DaySystem.CurrentDay == 2)
        {
            GameManager.SoundManager.musicSource.Stop();

            if (isOnApartment)
                GameManager.SoundManager.musicSource.clip = day2MusicApartment;
            else
                GameManager.SoundManager.musicSource.clip = day2MusicOutside;

            GameManager.SoundManager.musicSource.Play();
        }
    }

    public void RollCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void SetDayTwoAfterMarket()
    {
        day02beforeMarket.SetActive(false);
        day02afterMarket.SetActive(true);

        SecondDayDoor.enabled = true;
        SecondDayDoor.GetComponent<Door>().enabled = false;
    }

    public void AfterYıldırımCutscene()
    {
        SetDay(3);
        cinemachineConfiner.m_BoundingShape2D = thirdFloorBoundry;
        GameManager.Elevator.SetCurrentFloor(3);
        GameManager.DaySystem.SetIsOnOutside(false);
    }

    public void AfterManzaraCutscene()
    {
        cinemachineConfiner.m_BoundingShape2D = thirdFloorBoundry;
        finalCutscene.Play();
    }

    public void ActivateFinalCutsceneTrigger()
    {
        finalCutsceneTrigger.SetActive(true);
    }

    private void TeleportPlayerToKargaRoom()
    {
        GameManager.PlayerMovement.transform.position = kargoRoomPos;
        GameManager.Peri.transform.position = kargoRoomPos + GameManager.Peri.offset;
    }
}
