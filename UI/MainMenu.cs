using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Provides navigation to all user-accessible parts of the game.
public class MainMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button   startGameButton;
    public Button   levelButton;

    [Header("Set Dynamically")]
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
        levelButton.onClick.AddListener(delegate { NumericalSelectionMenu.S.ActivateMenu(); });
    }

    void StartGame() {
        // Start spawning objects
        ObjectSpawner.S.isSpawning = true;

        // Deactivate main menu
        gameObject.SetActive(false);

        // Unfreeze player
        PlayerManager.S.SetMobility(true);

        // Reset score
        ScoreManager.S.ResetScore();

        // Reset object spawner speeds
        ObjectSpawner.S.ResetSpeed();
    }

    public void StopGame() {
        // Stop objects from spawning
        ObjectSpawner.S.isSpawning = false;

        // Activate main menu
        gameObject.SetActive(true);

        // Freeze player
        PlayerManager.S.SetMobility(false);
    }
}