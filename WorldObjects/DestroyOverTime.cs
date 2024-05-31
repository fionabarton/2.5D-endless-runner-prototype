using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys an object within an adjustable amount of time.
public class DestroyOverTime : MonoBehaviour {
    [Header("Set in Inspector")]
    // Amount of time before object is destroyed
    public float    timeDuration = 15f;

    [Header("Set Dynamically")]
    // Specific time at which to destroy object
    private float   timeDone;

    void OnEnable() {
        // Set time to destroy object
        timeDone = timeDuration + Time.time;

        // Start timer
        StartCoroutine("FixedUpdateCoroutine");
    }

    //
    public IEnumerator FixedUpdateCoroutine() {
        // If time to destroy has passed...
        if (timeDone <= Time.time) {
            // ...destroy this GameObject
            Destroy(gameObject);
        }

        // Loop timer by restarting this coroutine
        yield return new WaitForFixedUpdate();
        StartCoroutine("FixedUpdateCoroutine");
    }
}