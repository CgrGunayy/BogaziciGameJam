using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static CamsHandler CamsHandler { get; private set; }
    public static DaySystem DaySystem { get; private set; }
    public static SoundManager SoundManager { get; private set; }
    public static DialogSystem DialogSystem { get; private set; }
    public static PlayerMovement PlayerMovement { get; private set; }
    public static Elevator Elevator { get; private set; }
    public static MainMenuSoundSettings SoundSetttings { get; private set; }
    public static SceneTransitionEffectHandler GameSceneTransitionEffect { get; private set; }
    public static PeriController Peri { get; private set; }

    private static GameObject civciv;
    private static DialogStarter tukan;
    private static DialogStarter evSahibi;

    private static Scene gameScene;

    private static int loadedMinigameIndex;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            CamsHandler = GameObject.FindObjectOfType<CamsHandler>();
            DialogSystem = GameObject.FindObjectOfType<DialogSystem>();
            DaySystem = GameObject.FindObjectOfType<DaySystem>();
            PlayerMovement = GameObject.FindObjectOfType<PlayerMovement>();
            Elevator = GameObject.FindObjectOfType<Elevator>();

            tukan = GameObject.FindGameObjectWithTag("Tukan").GetComponent<DialogStarter>();
            evSahibi = GameObject.FindGameObjectWithTag("EvSahibi").GetComponent<DialogStarter>();
            civciv = GameObject.FindGameObjectWithTag("Civciv");

            GameSceneTransitionEffect = GameObject.FindObjectOfType<SceneTransitionEffectHandler>();
            Peri = GameObject.FindObjectOfType<PeriController>();

            civciv.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (scene.name == "MainMenu")
        {
            SoundSetttings = GameObject.FindObjectOfType<MainMenuSoundSettings>();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        SoundManager = GameObject.FindObjectOfType<SoundManager>();
    }

    public static void LoadGameScene()
    {
        gameScene = default(Scene);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == "GameScene")
            {
                gameScene = scene;
                break;
            }
        }

        if (gameScene.isLoaded)
        {
            GameSceneTransitionEffect.FadeIn(PlayFadeToGameScene, loadedMinigameIndex);
        }
        else
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            GameSceneTransitionEffect.SetEffectCanvasActive(true);
            GameSceneTransitionEffect.FadeOut(true);
            SoundManager.SetMute(false);
        }

        if (loadedMinigameIndex == 1)
        {
            evSahibi.dialogIndex = 12;
            evSahibi.automaticTalk = true;
            evSahibi.gameObject.SetActive(false);
            evSahibi.gameObject.SetActive(true);
        }

        if(loadedMinigameIndex == 2)
        {
            tukan.dialogIndex += 1;
            tukan.automaticTalk = true;
            tukan.gameObject.SetActive(false);
            tukan.gameObject.SetActive(true);
        }

        if (loadedMinigameIndex == 3)
        {
            if(DaySystem.CurrentDay == 2)
            {
                DaySystem.day02YıldırımCutscene.Play();
            }
        }

        if (loadedMinigameIndex == 4)
        {
            civciv.gameObject.SetActive(true);

            if(DaySystem.CurrentDay == 1)
            {
                DaySystem.SecondDayDoor.enabled = true;
                DaySystem.SecondDayDoor.GetComponent<Door>().enabled = false;
            }

            if (DaySystem.CurrentDay == 2)
                DaySystem.SetDayTwoAfterMarket();
        }
    }

    public static void PlayFadeToGameScene(int minigameIndex)
    {
        SceneManager.UnloadSceneAsync(loadedMinigameIndex);
        SceneManager.SetActiveScene(gameScene);
        GameSceneTransitionEffect.SetEffectCanvasActive(true);
        GameSceneTransitionEffect.FadeOut(true);
        SoundManager.SetMute(false);
    }

    public static void PlayFadeInToMinigame(int index)
    {
        GameSceneTransitionEffect.FadeIn(LoadMinigame, index);
    }

    private static void LoadMinigame(int index)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        loadedMinigameIndex = index;
        AsyncOperation operation = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        operation.completed += MinigameAsyncOperationFinished;
    }

    private static void MinigameAsyncOperationFinished(AsyncOperation operation)
    {
        Scene operatingScene = default(Scene);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if(scene.buildIndex == loadedMinigameIndex)
            {
                operatingScene = scene;
                break;
            }
        }

        GameSceneTransitionEffect.SetEffectCanvasActive(false);
        SoundManager.SetMute(true);

        if (operatingScene != default(Scene))
            SceneManager.SetActiveScene(operatingScene);
        else
            Debug.LogWarning("Scene is null");
    }
}
