using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slowly rotates an object along the y-axis of its pivot point.
public class RotateObject : MonoBehaviour {
    void FixedUpdate() {
        gameObject.transform.Rotate(0, 90 * Time.fixedDeltaTime, 0,Space.Self);
    }
}