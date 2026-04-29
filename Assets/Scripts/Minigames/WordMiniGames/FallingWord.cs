using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Text))]
public class FallingWord : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private StringEvent OnClicked;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClicked?.Invoke(text.text);
    }

    [System.Serializable]
    private class StringEvent : UnityEvent<string> {}
}
