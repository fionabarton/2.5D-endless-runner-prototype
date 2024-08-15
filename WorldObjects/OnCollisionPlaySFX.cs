using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with an object, plays its attached audio source.
public class OnCollisionPlaySFX : MonoBehaviour {
    [Header("Set in Inspector")]
    public List <string> tagNames;

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        for(int i = 0; i < tagNames.Count; i++) {
            if (other.gameObject.tag == tagNames[i]) {
                AudioSource audio = other.gameObject.GetComponent<AudioSource>();
                if (audio) {
                    // Set volume to current SFX volume
                    //audio.volume = ...

                    // Play audio
                    audio.Play();
                }
            }
        }
    }
}