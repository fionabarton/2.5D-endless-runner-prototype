using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn an object after an adjustable amount of time has passed,
// then repeat endlessly.
public class ObjectSpawner : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<GameObject>     verticalObstacles;
    public GameObject           horizontalHighObstacle;
    public GameObject           horizontalLowObstacle;
    public GameObject           coinItem;
    public GameObject           shieldItem;

    // Amount of time before object is spawned
    public float                timeDuration = 2f;

    // Speed at which object moves forward
    public float                moveSpeed = 4;
    public float                startingMoveSpeed = 4;
    public float                amountToIncreaseMoveSpeed = 0.5f;

    // Amount of time before object is spawned
    public float                spawnSpeed = 4;
    public float                startingSpawnSpeed = 4;
    public float                amountToIncreaseSpawnSpeed = 0.5f;

    // -3, -1.5f, 0, 1.5f, 3
    public List<float>          itemSpawnXPositions = new List<float>();

    // -0.5f, 1, 2.5f, 4, 5.5f
    public List<float>          itemSpawnYPositions = new List<float>();

    [Header("Set Dynamically")]
    // Specific time at which to spawn object
    private float               timeDone;

    // Single instance of this class, which provides global acess from other scripts
    private static ObjectSpawner _S;
    public static ObjectSpawner S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void OnEnable() {
        // Set time to spawn object
        timeDone = spawnSpeed + Time.time;

        // Start timer
        StartCoroutine("FixedUpdateCoroutine");
    }

    private void Start() {
        // Initialize move & spawn speeds
        moveSpeed = startingMoveSpeed;
        spawnSpeed = startingSpawnSpeed;

        //
        InstantiateObject();
    }

    public IEnumerator FixedUpdateCoroutine() {
        // If time to spawn has passed...
        if (timeDone <= Time.time) {
            // ...spawn object
            InstantiateObject();

            // Reset time to spawn object
            timeDone = spawnSpeed + Time.time;
        }

        // Loop timer by restarting this coroutine
        yield return new WaitForFixedUpdate();
        StartCoroutine("FixedUpdateCoroutine");
    }

    void InstantiateObject() {
        // Get random value
        float randomVal = Random.value;

        // 3 slots: (evenly distributed)
        // Based on random value, select an object to instantiate
        //if (randomVal >= 0 && randomVal <= 0.3333f) {
        //    InstantiateHorizontalHighObstacle();
        //} else if (randomVal > 0.3333f && randomVal <= 0.6666f) {
        //    InstantiateCoin(); 
        //} else if (randomVal > 0.6666f && randomVal <= 1.0f) {
        //    InstantiateShield();
        //}

        // 4 slots: (evenly distributed)
        //if (randomVal >= 0 && randomVal <= 0.25f) {
        //    InstantiateHorizontalHighObstacle();
        //} else if (randomVal > 0.25f && randomVal <= 0.5f) {
        //    InstantiateHorizontalLowObstacle();
        //} else if (randomVal > 0.5f && randomVal <= 0.75f) {
        //    InstantiateRandomVerticalObstacle();
        //} else if (randomVal > 0.75f && randomVal <= 1.0f) {
        //    InstantiateCoin();
        //}

        // 5 slots: (evenly distributed)
        //if (randomVal >= 0 && randomVal <= 0.2f) {
        //    InstantiateHorizontalHighObstacle();
        //} else if (randomVal > 0.2f && randomVal <= 0.4f) {
        //    InstantiateHorizontalLowObstacle();
        //} else if (randomVal > 0.4f && randomVal <= 0.6f) {
        //    InstantiateRandomVerticalObstacle();
        //} else if (randomVal > 0.6f && randomVal <= 0.8f) {
        //    InstantiateCoin();
        //} else if (randomVal > 0.8f && randomVal <= 1.0f) {
        //    InstantiateShield();
        //}

        // 5 slots: (individually weighted)
        if (randomVal >= 0 && randomVal <= 0.2f) {
            InstantiateHorizontalHighObstacle();
        } else if (randomVal > 0.2f && randomVal <= 0.4f) {
            InstantiateHorizontalLowObstacle();
        } else if (randomVal > 0.4f && randomVal <= 0.6f) {
            InstantiateRandomVerticalObstacle();
        } else if (randomVal > 0.6f && randomVal <= 0.9f) {
            InstantiateCoin();
        } else if (randomVal > 0.9f && randomVal <= 1.0f) {
            InstantiateShield();
        }
    }

    //
    void InstantiateHorizontalHighObstacle() {
        Instantiate(horizontalHighObstacle, new Vector3(0, 6.25f, 10), Quaternion.identity * horizontalHighObstacle.transform.localRotation);
    }

    //
    void InstantiateHorizontalLowObstacle() {
        Instantiate(horizontalLowObstacle, new Vector3(0, 2.5f, 10), Quaternion.identity * horizontalLowObstacle.transform.localRotation);
    }

    //
    void InstantiateRandomVerticalObstacle() {
        // Get random index
        int ndx = Random.Range(0, 3);

        if (ndx == 0) {
            Instantiate(verticalObstacles[ndx], new Vector3(-3f, 5.25f, 10), Quaternion.identity * verticalObstacles[ndx].transform.localRotation);
        } else if (ndx == 1) {
            Instantiate(verticalObstacles[ndx], new Vector3(0, 5.25f, 10), Quaternion.identity * verticalObstacles[ndx].transform.localRotation);
        } else if (ndx == 2) {
            Instantiate(verticalObstacles[ndx], new Vector3(3, 5.25f, 10), Quaternion.identity * verticalObstacles[ndx].transform.localRotation);
        }  
    }

    // Instatiate coin item at a randomized position
    void InstantiateCoin() {
        Instantiate(coinItem, GetRandomItemPosition(), Quaternion.identity * coinItem.transform.localRotation);
    }

    // Instantiate shield item at a randomized position
    void InstantiateShield() {
        Instantiate(shieldItem, GetRandomItemPosition(), Quaternion.identity * coinItem.transform.localRotation);
    }

    // Get and return a random valid 2D position at which to spawn an item (coin or shield)
    Vector3 GetRandomItemPosition() {
        // Get random values for both x & y-axes
        int x = Random.Range(0, 4);
        int y = Random.Range(0, 4);

        // Return position with random values
        return new Vector3(itemSpawnXPositions[x], itemSpawnYPositions[y], 10);
    }

    // Increment object move and spawn speeds by an adjustable amount
    public void IncrementSpeed() {
        moveSpeed += amountToIncreaseMoveSpeed;
        spawnSpeed -= amountToIncreaseSpawnSpeed;
    }

    // Reset object move and spawn speeds 
    public void ResetSpeed() {
        moveSpeed = startingMoveSpeed;
        spawnSpeed = startingSpawnSpeed;
    }
}