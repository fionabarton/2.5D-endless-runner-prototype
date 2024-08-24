using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves an object in a single, specified direction at a specified speed.
public class TranslateObject : MonoBehaviour {
    [Header("Set in Inspector")]
    public Rigidbody    rigid;

    [Header("Set Dynamically")]
    public bool         isMoving = true;

    public Vector3      direction = Vector3.forward;

    void FixedUpdate() {
        // Translate object towards desired direction
        if (isMoving) {
            transform.Translate(direction * Time.fixedDeltaTime * ObjectSpawner.S.moveSpeed);
        }
    }

    public void SetDirection(Vector3 _direction) {
        // Reset velocity
        rigid.velocity = Vector3.zero;

        direction = _direction;
    }
}