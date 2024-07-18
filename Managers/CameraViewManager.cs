using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On keyboard input, adjusts fixed camera position and rotation.
public class CameraViewManager : MonoBehaviour {
    [Header("Set Dynamically")]
    public int currentViewNdx = 0;
    
    void Update() {
        // On keyboard input...
        if (Input.GetKeyDown(KeyCode.C)) {
            // Increment currentViewNdx
            if (currentViewNdx >= 5) {
                currentViewNdx = 0;
            } else {
                currentViewNdx += 1;
            }

            // Adjust camera position and rotation
            if (currentViewNdx == 0) {
                transform.localPosition = new Vector3(0, 1.5f, -10);
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else if (currentViewNdx == 1) {
                transform.localPosition = new Vector3(0, 5, -8);
                transform.eulerAngles = new Vector3(22.5f, 0, 0);
            } else if (currentViewNdx == 2) {
                transform.localPosition = new Vector3(0, -2.5f, -8);
                transform.eulerAngles = new Vector3(-22.5f, 0, 0);
            } else if (currentViewNdx == 3) {
                transform.localPosition = new Vector3(5, 1.5f, -8);
                transform.eulerAngles = new Vector3(0, -22.5f, 0);
            } else if (currentViewNdx == 4) {
                transform.localPosition = new Vector3(5, 5, -8);
                transform.eulerAngles = new Vector3(22.5f, -22.5f, 0);
            } else if (currentViewNdx == 5) {
                transform.localPosition = new Vector3(5, -2, -8);
                transform.eulerAngles = new Vector3(-22.5f, -22.5f, 0);
            }
        }
    }
}