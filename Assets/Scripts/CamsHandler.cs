using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamsHandler : MonoBehaviour
{
    public Camera CurrentCamera { get; private set; }

    [SerializeField] private Camera startCamera;
    [SerializeField] private float transitionDelay;
    [SerializeField] private float teleportDelay;
    [Space]
    public Cinemachine.CinemachineVirtualCamera vCam;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private Animator fadePanelAnim;

    private List<Camera> allCams = new List<Camera>();
    private WaitForSeconds transitionDelayer;
    private WaitForSeconds teleportDelayer;

    private void Awake()
    {
        foreach (GameObject camObj in GameObject.FindGameObjectsWithTag("Camera"))
        {
            allCams.Add(camObj.GetComponent<Camera>());
        }

        transitionDelayer = new WaitForSeconds(transitionDelay);
        teleportDelayer = new WaitForSeconds(teleportDelay);
    }

    private void Start()
    {
        TurnOffAllCams();
        TransitionTo(startCamera, false);
    }

    public void TransitionTo(Camera newCam, bool fadeEffect = true, System.Action transitionAction = null)
    {
        if(fadeEffect)
            StartCoroutine(TransitionEffect(newCam, transitionAction));
        else
        {
            SetCurrentCam(newCam);
            transitionAction?.Invoke();
        }         
    }

    public void TurnOffAllCams()
    {
        allCams.ForEach((cam) => cam.gameObject.SetActive(false));
    }

    private void SetCurrentCam(Camera newCam)
    {
        CurrentCamera?.gameObject.SetActive(false);
        newCam.gameObject.SetActive(true);
        audioListener.transform.SetParent(newCam.transform);
        CurrentCamera = newCam;
    }

    private IEnumerator TransitionEffect(Camera newCam = null, System.Action transitionAction = null)
    {
        fadePanelAnim.SetBool("FadeIn", true);
        yield return teleportDelayer;
        transitionAction?.Invoke();
        if(newCam != null)
            SetCurrentCam(newCam);
        yield return transitionDelayer;
        fadePanelAnim.SetBool("FadeIn", false);
    }
}
