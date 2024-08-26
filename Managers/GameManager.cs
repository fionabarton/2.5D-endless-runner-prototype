using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Handles general game functionality such as scene management and pausing the game.
public class GameManager : MonoBehaviour {
    [Header("Set Dynamically")]
    // Stores whether game is paused
    //public bool         paused;

    // Transparent black image that covers entire screen when game paused
    //public GameObject   pauseBlackScreen;

    // Selected GameObject
    GameObject lastselect;

    // Screen Resolution
    Vector2 resolution;

    // Single instance of this class, which provides global acess from other scripts
    private static GameManager _S;
    public static GameManager S { get { return _S; } set { _S = value; } }

    // Single instance of whether or not this object already exists
    public static bool  exists;

    void Awake(){
        // Populate singleton with this instance
        S = this;

        // If an instance of this object doesn't already exist...
        if (!exists) {
            // On new scene load, do not destroy this object
            exists = true;
            DontDestroyOnLoad(gameObject);
        } else {
            // Destroy this object
            Destroy(gameObject);
        }

        // Disable mouse input
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    //void Update() {
    //    // On 'p' key pressed...
    //    if (Input.GetButtonDown("Pause")) {
    //        if (!paused) {
    //            // ...if not paused, pause game and freeze time scale.
    //            paused = true;
    //            Time.timeScale = 0.0f;

    //            // Activate transparent black image covering entire screen
    //            pauseBlackScreen.SetActive(true);     
    //        } else {
    //            // ...if paused, unpause game and reset time scale.
    //            paused = false;
    //            Time.timeScale = 1.0f;

    //            // Deactivate transparent black image covering entire screen
    //            pauseBlackScreen.SetActive(false);               
    //        }
    //    }
    //}

    void FixedUpdate() {
        // If screen resolution changes...
        if (resolution.x != Screen.width || resolution.y != Screen.height) {
            // ... change screen width & height to update position/size of UI 
            resolution.x = Screen.width;
            resolution.y = Screen.height;
        }

        // Mouse input is disabled by deactivating Canvas > Graphic Raycaster...
        // ...this handles if a left mouse click results in currently selected GO == null
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == null) {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(lastselect);
        } else {
            lastselect = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        }
    }

    // Find and destroy all moving objects (obstacles, coins, & shields)
    public void DestroyAllObjects() {
        // Find all obstacles and items
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        // Destroy all obstacles and items
        for (int i = 0; i < obstacles.Length; i++) {
            Destroy(obstacles[i]);
        }
        for (int i = 0; i < items.Length; i++) {
            Destroy(items[i]);
        }
    }

    // Get midpoint between two gameObjects
    public Vector3 GetMidpoint(GameObject object1, GameObject object2) {
        Vector3 midpoint = object2.transform.position + (object1.transform.position - object2.transform.position) / 2;
        return midpoint;
    }

    // Set Selected GameObject
    public void SetSelectedGO(GameObject tGO) {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(tGO);
    }
}