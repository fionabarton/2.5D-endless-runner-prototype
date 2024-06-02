using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Actions performed by camera
public enum eCamMode { freeze, followTarget };

// Handles camera movement and positioning.
public class CameraManager : MonoBehaviour {
    [Header("Set in Inspector")]
    // Target transform to be followed by camera
    public Transform    targetTrans;

    [Header("Set Dynamically")]
    // Current mode of action performed by camera
    public eCamMode     mode;

    // Cached position at which to set frozen camera
    public float        freezePosX;
    public float        freezePosY;

    // Optionally smooth camera movement
    public bool         canLerp;
    private float       easing = 0.15f;
    private Vector3     velocity = Vector3.zero;

    // Optionally clamp camera position between two points on the x-axis
    public bool         canClampPosX;
    public float        minPosX;
    public float        maxPosX;

    // Optionally clamp camera position between two points on the y-axis
    public bool         canClampPosY;
    public float        minPosY;
    public float        maxPosY;

    // Dynamic value that camera position is set to every LateUpdate() 
    private Vector3     destination;

    // Single instance of this class, which provides global acess from other scripts
    private static CameraManager _S;
    public static CameraManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        // If (targetTrans) null, log error message to console
        if (!targetTrans) {
            Debug.LogError("targetTrans has NOT been assigned a transform in the Inspector.");
        }
    }

    // Set camera position to a specific 2D location
    public void SetCamPosition(Vector2 targetPos) {
        freezePosX = targetPos.x;
        freezePosY = targetPos.y;
    }

    void LateUpdate() {
        // Depending on mode...
        switch (mode) {
            case eCamMode.freeze:
                // ...set (destination) to cached 2D values
                destination.x = freezePosX;
                destination.y = freezePosY;
                break;
            case eCamMode.followTarget:
                // ...set (destination) to location of (targetTrans)
                destination = targetTrans.localPosition;
                break;
        }

        if (canClampPosX) {
            // Clamp camera position on x-axis
            destination.x = Mathf.Max(destination.x, minPosX);
            destination.x = Mathf.Min(destination.x, maxPosX);
        }

        if (canClampPosY) {
            // Clamp camera position on y-axis
            destination.y = Mathf.Max(destination.y, minPosY);
            destination.y = Mathf.Min(destination.y, maxPosY);
        }

        if (canLerp) {
            // Gradually change current camera position toward (destination)
            destination = Vector3.SmoothDamp(transform.localPosition, destination, ref velocity, easing);
        }

        // Ensure position on z-axis maintains default value
        destination.z = -10;

        // Set camera position to (destination)
        transform.localPosition = destination;
    }
}