using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Functions/FinalTriggerActivator")]
public class FinalCutsceneActivator : ScriptableObject
{
    public void ActivateFinalCutscene()
    {
        GameManager.DaySystem.ActivateFinalCutsceneTrigger();
    }
}
