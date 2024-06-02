using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On trigger enter, move player GameObject back to starting position.
public class RespawnTrigger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll) {
        // If tag of (coll) GameObject is (Player)...
        if (coll.gameObject.tag == "Player") {
            // ...reset player position
			PlayerManager.S.MoveToStartingPosition();
        }
	}
}