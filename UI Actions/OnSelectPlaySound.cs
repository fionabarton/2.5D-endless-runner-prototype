using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// On this UI object selected, play sound.
public class OnSelectPlaySound : MonoBehaviour, ISelectHandler {
    [Header("Set in Inspector")]
    public AudioSource audioSource;

    public void OnSelect(BaseEventData eventData) {
        audioSource.Play();
    }
}