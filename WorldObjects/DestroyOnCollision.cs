using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with objects of a specified Unity tag, destroy this GameObject.
public class DestroyOnCollision : MonoBehaviour {
    [Header("Set in Inspector")]
    public string tagName = "Player";

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            // ...destroy this GameObject
            Destroy(gameObject);
        }
    }
}