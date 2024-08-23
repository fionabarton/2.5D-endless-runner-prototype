using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Provides navigation to all user-accessible parts of the game.
public class MainMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button                   startGameButton;
    public Button                   levelButton;
    public Button                   highScoreMenuButton;
    public Button                   optionsMenuButton;

    public TMPro.TextMeshProUGUI    levelValueText;


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
        highScoreMenuButton.onClick.AddListener(delegate { HighScoreMenu.S.ActivateMenu(true); });
        optionsMenuButton.onClick.AddListener(delegate { OptionsMenu.S.ActivateMenu(); });

        UpdateUI();
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

        // Set level
        ScoreManager.S.SetLevel(ScoreManager.S.startingLevel - 1);

        // Play BGM
        AudioManager.S.PlayBGM(eBGMAudioClipName.gameplay);

        // Announce "let's go!"
        AnnouncerManager.S.DisplayLetsGo();
    }

    public void Activate(bool playSFX) {
        // Play BGM
        if (playSFX) {
            AudioManager.S.PlayBGM(eBGMAudioClipName.mainMenu);
        }

        // Activate main menu
        gameObject.SetActive(true);
    }

    // After level selection, update level, move, & spawn speed text values
    public void UpdateUI() {
        levelValueText.text = ScoreManager.S.level + "\n" + ObjectSpawner.S.moveSpeed + "\n" + ObjectSpawner.S.spawnSpeed;
    }
}