using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Provides navigation to all user-accessible parts of the game.
public class MainMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button   startGameButton;

    // Single instance of this class, which provides global acess from other scripts
    private static MainMenu _S;
    public static MainMenu S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        // Add listeners to buttons
        startGameButton.onClick.AddListener(delegate { StartGame(); });
    }

    void StartGame() {
        // Start spawning objects
        ObjectSpawner.S.isSpawning = true;

        // Deactivate main menu
        gameObject.SetActive(false);    
    }

    public void StopGame() {
        // Stop objects from spawning
        ObjectSpawner.S.isSpawning = false;

        // Activate main menu
        gameObject.SetActive(true);
    }
}