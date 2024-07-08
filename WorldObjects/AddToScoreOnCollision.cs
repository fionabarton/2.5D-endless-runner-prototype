using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with objects of a specified Unity tag, add to the user's score.
public class AddToScoreOnCollision : MonoBehaviour {
    [Header("Set in Inspector")]
    public string tagName = "Player";

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            // ...add to the user's score...
            ScoreManager.S.AddToScore();

            // ...and destroy this GameObject
            Destroy(gameObject);
        }
    }
}