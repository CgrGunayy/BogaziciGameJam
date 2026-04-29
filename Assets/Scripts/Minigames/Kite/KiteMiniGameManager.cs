using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteMiniGameManager : MonoBehaviour
{
    public bool Started { get; private set; }

    [SerializeField] private KiteCamera kiteCam;
    [SerializeField] private Transform peri;

    private float camStartVelocity;
    private Vector2 camStartPos;
    private Vector2 periStartPos;
    
    private void Start()
    {
        camStartPos = kiteCam.transform.position;
        periStartPos = peri.transform.position;
        camStartVelocity = kiteCam.speed;
        
        if(FindObjectOfType<AudioListener>() == null)
        {
            gameObject.AddComponent<AudioListener>();
        }
    }

    private void Update()
    {
        if (Started)
        {
            kiteCam.ElevateCamera();
        }
    }

    public void StartGame()
    {
        Started = true;
    }

    public void GameOver()
    {
        kiteCam.CamShaker.shakeDuration = 0.1f;
        kiteCam.transform.position = camStartPos;
        peri.position = periStartPos;
        kiteCam.speed = camStartVelocity;
    }

    public IEnumerator GameWin()
    {
        yield return new WaitForSeconds(0.2f);
        peri.GetComponent<PeriMouseFollow>().enabled = false;
        yield return new WaitForSeconds(1f);
        GameManager.PlayerMovement?.SetCanWalk(true);
        GameManager.LoadGameScene();
    }
}
