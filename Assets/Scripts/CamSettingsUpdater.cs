using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSettingsUpdater : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private CinemachineConfiner confiner;
    [SerializeField] private float newCamSize;

    public Collider2D borderCollider;

    public void ApplySettings()
    {
        vCam.m_Lens.OrthographicSize = newCamSize;
        confiner.m_BoundingShape2D = borderCollider;
    }
}
