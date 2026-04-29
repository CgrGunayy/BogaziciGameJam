using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionEffectHandler : MonoBehaviour
{
    [SerializeField] private Animator fadePanelAnim;
    [SerializeField] private float transitionDelay;
    [SerializeField] private Canvas effectCanvas;

    private WaitForSeconds delayer;

    private void Awake()
    {
        delayer = new WaitForSeconds(transitionDelay);
    }

    public void FadeIn(System.Action<int> action, int value)
    {
        fadePanelAnim.SetBool("FadeIn", true);
        StartCoroutine(Delay(action, value));
    }

    private IEnumerator Delay(System.Action<int> action, int value)
    {
        yield return delayer;
        action?.Invoke(value);
    }
  
    public void SetEffectCanvasActive(bool active)
    {
        effectCanvas.gameObject.SetActive(active);
    }

    public void FadeOut(bool instant)
    {
        if (instant)
            fadePanelAnim.SetTrigger("FadeOut");
        else
            fadePanelAnim.SetBool("FadeIn", false);
    }

}
