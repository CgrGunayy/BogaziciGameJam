using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Dialog")]
public class Dialog : ScriptableObject
{
    public Speech[] dialog;
    [Space]
    public bool onlyOneDialog = false;
    public UnityEvent dialogFinishEvent;

    private void OnValidate()
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            dialog[i].UpdateAutomaticDelayer();
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            dialog[i].UpdateAutomaticDelayer();
        }
    }
}
