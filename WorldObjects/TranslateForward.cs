using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an object forward on the z-axis at an adjustable rate.
public class TranslateForward : MonoBehaviour {
    [Header("Set in Inspector")]
    public float    moveSpeed = 1;

    void FixedUpdate() {
        // Translate object forward on z-axis
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * moveSpeed);
    }
}