using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObjects/Functions/MinigameLoader")]
public class MinigameLoader : ScriptableObject
{
    public void LoadMinigame(int index)
    {
        GameManager.PlayFadeInToMinigame(index);
    }
}
