using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with objects of a specified Unity tag, activate the player's shield,
// which prevents the game from ending after they've collided with a single red obstacle.
public class AddShieldOnCollision : MonoBehaviour {
    [Header("Set in Inspector")]
    public string tagName = "Player";

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            // ...activate the player's shield...
            PlayerManager.S.ActivateShield();

            // ...and destroy this GameObject
            Destroy(gameObject);
        }
    }
}