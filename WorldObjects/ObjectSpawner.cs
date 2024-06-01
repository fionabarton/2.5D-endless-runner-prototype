using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn an object after an adjustable amount of time has passed,
// then repeat endlessly.
public class ObjectSpawner : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject   objectToSpawn;

    // Amount of time before object is spawned
    public float        timeDuration = 2f;

    [Header("Set Dynamically")]
    // Specific time at which to spawn object
    private float       timeDone;

    void OnEnable() {
        // Set time to spawn object
        timeDone = timeDuration + Time.time;

        // Start timer
        StartCoroutine("FixedUpdateCoroutine");
    }

    public IEnumerator FixedUpdateCoroutine() {
        // If time to spawn has passed...
        if (timeDone <= Time.time) {
            // ...spawn object
            SpawnObjectAtRandomPosition();

            // Reset time to spawn object
            timeDone = timeDuration + Time.time;
        }

        // Loop timer by restarting this coroutine
        yield return new WaitForFixedUpdate();
        StartCoroutine("FixedUpdateCoroutine");
    }

    // Instatiate objectToSpawn at a randomized position
    void SpawnObjectAtRandomPosition() {
        Instantiate(objectToSpawn, GetRandomPosition(), Quaternion.identity * objectToSpawn.transform.localRotation);
    }

    // Get and return a random position within a specified range
    Vector3 GetRandomPosition() {
        // Get random values for both x & y-axes
        int x = Random.Range(-5, 5);
        int y = Random.Range(0, 5);

        // Return position with random values
        return new Vector3(x, y, 5);
    }
}