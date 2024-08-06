using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with objects of a specified Unity tag, ends the game.
public class OnCollisionGameOver : MonoBehaviour {
    [Header("Set in Inspector")]
    public string tagName = "Player";

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            if (!PlayerManager.S.isShielded) {
                // ...end the game
                PlayerManager.S.GameOver();
            } else {
                // ...damage and...
                PlayerManager.S.Damaged();

                // ...deactivate the player's shield
                PlayerManager.S.DeactivateShield();
            } 
        }
    }
}