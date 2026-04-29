using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashierMiniGameManager : MonoBehaviour
{
    public static CashierMiniGameManager Instance { get; private set; }

    [SerializeField] private Slider progressSlider;
    [SerializeField] private Animator warningPanelAnim;
    [SerializeField] private ProductSpawner productSpawner;

    private int progress;
    private SceneTransitionEffectHandler sceneTransitionEffectHandler;

    public Camera MainCamera { get; private set; }

    private void Awake()
    {
        Instance = this;
        MainCamera = Camera.main;
        sceneTransitionEffectHandler = GameObject.FindObjectOfType<SceneTransitionEffectHandler>();
    }

    private void Start()
    {
        sceneTransitionEffectHandler.FadeOut(true);
        progress = 10;
        RefreshDisplay();

        if (FindObjectOfType<AudioListener>() == null)
            gameObject.AddComponent<AudioListener>();

    }

    private void RefreshDisplay()
    {
        progressSlider.value = progress;
    }

    public void AddProgress(int amount)
    {
        progress += amount;
        RefreshDisplay();

        if (amount < 0)
            warningPanelAnim.SetTrigger("Warning");

        if (progress <= 0)
            GameOver();
        else if (progress >= 100)
            GameWin();
        else if(progress >= 35 && productSpawner.spawnDelay > 1.5f)
        {
            productSpawner.spawnDelay -= 0.1f;
        }
    }

    private void GameOver()
    {
        productSpawner.Restart();
        progress = 10;
        RefreshDisplay();
    }

    private void GameWin()
    {
        productSpawner.StopGeneration();
        productSpawner.ClearProducts();
        GameManager.LoadGameScene();
    }
}
