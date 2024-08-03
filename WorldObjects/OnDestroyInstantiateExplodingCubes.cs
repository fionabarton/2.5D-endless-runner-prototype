using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// On destroy, instantiate a barrage of exploding red cubes.
public class OnDestroyInstantiateExplodingCubes : MonoBehaviour {
    [Header("Set dynamically")]
    public GameObject explodingCubes;

    private void OnDestroy() {
        Instantiate(explodingCubes, transform.position, transform.rotation);
    }
}