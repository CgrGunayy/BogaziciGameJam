using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogContainer")]
public class DialogContainer : ScriptableObject
{
    public Dialog[] dialogs;
    [ReadOnly] public List<int> spokenDialogs = new List<int>();
}
