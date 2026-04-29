using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Transform menuPosition;
    [SerializeField] private Transform settingsPosition;

    [SerializeField] private GameObject cam;
    public float lerpSpeed;

    [Header("Ses")]
    [SerializeField] private AudioClip clickSounds;

    private bool camIsMoving = false;
    private Vector2 currentPoint;
    private bool isCanClickToGoMenuBtnAgain = true;

    public void ClickToGoMenu()
    {
        GameManager.SoundManager.PlaySfx(clickSounds);
        MoveCam(menuPosition.position);
    }
    public void StartGame()
    {
        GameManager.SoundManager.PlaySfx(clickSounds);
        SceneManager.LoadScene("GameScene"); //Debug
    }
    public void Settings()
    {
        if (!camIsMoving)
        {
            GameManager.SoundManager.PlaySfx(clickSounds);
            MoveCam(settingsPosition.position);
        }
    }
    public void BackToMenu()
    {
        if (!camIsMoving)
        {
            GameManager.SoundManager.PlaySfx(clickSounds);
            MoveCam(menuPosition.position);
        }
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
    private void MoveCam(Vector2 point)
    {
        currentPoint = point;
        camIsMoving = true;
    }
    private void Update()
    {
        if (camIsMoving)
        {
            Vector2 vec2Pos = Vector2.Lerp(cam.transform.position, currentPoint, lerpSpeed * Time.deltaTime);
            cam.transform.position = new Vector3(vec2Pos.x, vec2Pos.y, cam.transform.position.z);
            if (Vector2.Distance(cam.transform.position,currentPoint) < 0.1f)
                camIsMoving = false;
        }

        if (Input.anyKeyDown)
        {
            if (isCanClickToGoMenuBtnAgain)
            {
                ClickToGoMenu();
                isCanClickToGoMenuBtnAgain = false;
            }
        }
    }
}
