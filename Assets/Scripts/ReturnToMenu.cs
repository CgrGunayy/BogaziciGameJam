using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    WaitForSeconds wfs;

    private void Start()
    {
        wfs = new WaitForSeconds(2f);
    }

    public void OnReturnToMenu()
    {
        StartCoroutine(ReturnAfterDelay());
    }

    private IEnumerator ReturnAfterDelay()
    {
        yield return wfs;
        SceneManager.LoadScene("MainMenu");
    }
}
