using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On trigger enter, set player GameObject to hold onto and hang from this hanger.
public class Hanger : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D coll) {
		// If (coll) has an attached PlayerManager component...
		PlayerManager collPlayer = coll.gameObject.GetComponent<PlayerManager>();
		if (collPlayer != null) {
			// ...player grabs and holds onto hanger
			PlayerManager.S.GrabHanger();

			// Set player position on hanger
			PositionPlayerOnHanger();
        }
	}

	void OnTriggerExit2D(Collider2D coll) {
        // If (coll) has an attached PlayerManager component...
        PlayerManager collPlayer = coll.gameObject.GetComponent<PlayerManager>();
		if (collPlayer != null) {
            // ...player lets go of and detaches from hanger
            PlayerManager.S.ReleaseHanger();
        }
	}

	// Move player to hold onto hanger
	void PositionPlayerOnHanger() {
        Vector3 tPos = PlayerManager.S.transform.localPosition;
        tPos.x = transform.localPosition.x;
        tPos.y = transform.localPosition.y - 0.4f;
        PlayerManager.S.transform.localPosition = tPos;
    }
}