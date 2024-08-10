using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an object forward on the z-axis at an adjustable rate.
public class TranslateForward : MonoBehaviour {
    [Header("Set Dynamically")]
    public bool isMoving = true;

    void FixedUpdate() {
        // Translate object forward on z-axis
        if (isMoving) {
            transform.Translate(Vector3.forward * Time.fixedDeltaTime * ObjectSpawner.S.moveSpeed);
        }  
    }
}