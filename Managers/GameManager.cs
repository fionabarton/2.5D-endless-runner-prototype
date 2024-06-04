﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Handles general game functionality such as scene management and pausing the game.
public class GameManager : MonoBehaviour {
    [Header("Set in Inspector")]
    // First scene to be loaded on Awake()
    public string       firstScene;

    [Header("Set Dynamically")]
    // Stores whether game is paused
    public bool         paused;

    // Transparent black image that covers entire screen when game paused
    public GameObject   pauseBlackScreen;

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

        // Load first scene
        SceneManager.LoadScene(firstScene);
    }

    void Update() {
        // On 'p' key pressed...
        if (Input.GetButtonDown("Pause")) {
            if (!paused) {
                // ...if not paused, pause game and freeze time scale.
                paused = true;
                Time.timeScale = 0.0f;

                // Activate transparent black image covering entire screen
                pauseBlackScreen.SetActive(true);     
            } else {
                // ...if paused, unpause game and reset time scale.
                paused = false;
                Time.timeScale = 1.0f;

                // Deactivate transparent black image covering entire screen
                pauseBlackScreen.SetActive(false);               
            }
        }
    }
}