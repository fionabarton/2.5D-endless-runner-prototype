using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// On this UI object clicked, play sound
public class OnClickPlaySound : MonoBehaviour, IPointerClickHandler {
    [Header("Set in Inspector")]
    public AudioSource audioSource;

    public void OnPointerClick(PointerEventData pointerEventData) {
        audioSource.Play();
    }
}