using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Person")]
public class Person : ScriptableObject
{
    public string personName;
    public Sprite personImage;
}
