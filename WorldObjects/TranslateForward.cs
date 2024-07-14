using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an object forward on the z-axis at an adjustable rate.
public class TranslateForward : MonoBehaviour {
    void FixedUpdate() {
        // Translate object forward on z-axis
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * ObjectSpawner.S.moveSpeed);
    }
}