using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision with objects of a specified Unity tag, add to the user's score.
public class OnCollisionAddToScore : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject   destructionParticleSystem;

    public string       tagName = "Player";

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            // ...add to the user's score...
            ScoreManager.S.AddToScore();

            // ...instantiate particle system...
            Instantiate(destructionParticleSystem, GameManager.S.GetMidpoint(gameObject, other.gameObject), transform.rotation);

            // ...and destroy this GameObject
            Destroy(gameObject);
        }
    }
}