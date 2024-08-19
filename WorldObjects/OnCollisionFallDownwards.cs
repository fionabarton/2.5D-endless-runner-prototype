using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On collision stop object movement and enable object gravity.
public class OnCollisionFallDownwards : MonoBehaviour {
    [Header("Set in Inspector")]
    public string   tagName = "KillPlane";

    public bool     isObstacle;

    private void OnTriggerEnter(Collider other) {
        // If tag of (other) GameObject matches (tagName)...
        if (other.gameObject.tag == tagName) {
            if (isObstacle) {
                // Redirect object movement downwards
                TranslateObject to = GetComponent<TranslateObject>();
                if (to) {
                    to.SetDirection(Vector3.down);
                }

                // Destroy over time
                DestroyOverTime dot = GetComponent<DestroyOverTime>();
                if (dot) {
                    dot.StartTimerToDestruction();
                }

                // Activate object gravity, resulting in it falling downwards
                //Rigidbody rb = GetComponent<Rigidbody>();
                //if (rb) {
                //    rb.useGravity = true;
                //}
            } else {
                // Redirect object movement downwards
                TranslateObject to = GetComponentInParent<TranslateObject>();
                if (to) {
                    to.SetDirection(Vector3.down);
                }

                // Destroy over time
                DestroyOverTime dot = GetComponentInParent<DestroyOverTime>();
                if (dot) {
                    dot.StartTimerToDestruction();
                }

                // Activate object gravity, resulting in it falling downwards
                //Rigidbody rb = GetComponent<Rigidbody>();
                //if (rb) {
                //    rb.useGravity = true;
                //}
            }
        } 
    }
}