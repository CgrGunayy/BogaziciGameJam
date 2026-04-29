using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Speech
{
    public Person speaker;
    [Space(20)]
    [Multiline] public string content;
    [Space(20)]
    public bool onAutomaticSkip = false;
    public float automaticSkipDelay = 0;
    [Space(20)]
    public AudioClip speechSound;

    public WaitForSeconds AutomaticSkipDelayer { get; private set; }

    public void UpdateAutomaticDelayer()
    {
        if (onAutomaticSkip)
            AutomaticSkipDelayer = new WaitForSeconds(automaticSkipDelay);
        else
            AutomaticSkipDelayer = null;
    }
}
