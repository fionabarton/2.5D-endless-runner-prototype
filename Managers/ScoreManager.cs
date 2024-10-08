using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Caches and displays the user's current score.
public class ScoreManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI    scoreText;
    public TMPro.TextMeshProUGUI    levelText;
    public TMPro.TextMeshProUGUI    objectSpawnCountText;
    public TMPro.TextMeshProUGUI    runTimeText;

    [Header("Set Dynamically")]
    public int                      score = 0;
    public int                      level = 1;
    public int                      objectCount = 0;
    public int                      startingLevel = 1;
    public float                    startingTime = 0f;
    public float                    endingTime = 0f;

    // Current amount of coins required to reach next level
    public int                      amountToNextLevel = 5;

    // Single instance of this class, which provides global acess from other scripts
    private static ScoreManager     _S;
    public static ScoreManager      S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    private void FixedUpdate() {
        // Set run time text
        if (ObjectSpawner.S.isSpawning) {
            runTimeText.text = "Time:<color=white> " + GetTime(Time.time);
        }
    }

    // Reset the user's score to default values
    public void ResetScore() {
        score = 0;
        level = startingLevel;
        objectCount = 0;
        startingTime = Time.time;
        endingTime = 0f;
        amountToNextLevel = 5;

        UpdateGUI();
    }

    // Add one point to the user's score and update scoreboard UI
    public void AddToScore() {
        // Increment score and update local UI
        score += 1;
        UpdateGUI();

        amountToNextLevel -= 1;

        // Every 5 points, go to next level
        if (score % 5 == 0) {
            // Proceed to next level
            NextLevel();
        } else {
            // Play SFX
            AudioManager.S.PlaySFX(eSFXAudioClipName.grabCoin);

            // Display text of a random exclamation
            AnnouncerManager.S.DisplayRandomExclamation();
        }
    }

    // Amount of total objects spawned
    public void AddToObjectCount(int amount = 1) {
        objectCount += amount;

        objectSpawnCountText.text = "Objects:<color=white> " + objectCount;
    }

    //
    public void NextLevel() {
        // Increment level and update UI
        level += 1;
        UpdateGUI();

        // Increment object move and spawn speeds
        ObjectSpawner.S.IncrementSpeed();

        // Reset amountToNextLevel 
        amountToNextLevel = 5;

        // Display text that the player has reached the next level
        AnnouncerManager.S.DisplayNextLevel();

        // Drop confetti
        ConfettiManager.S.DropConfetti();

        // Set color palette
        ColorManager.S.GetNewColorPalette();

        // Play SFX
        AudioManager.S.PlaySFX(eSFXAudioClipName.applause);
        AudioManager.S.PlaySFX(eSFXAudioClipName.grabCoinLevelUp);
    }

    // On level select, increment game speeds and update GUI
    public void SetLevel(int levelNdx = 0) {
        // Reset spawner speeds
        ObjectSpawner.S.ResetSpeed();

        // Increment object move & spawn speeds
        if (levelNdx != 0) {
            for (int i = 0; i < levelNdx; i++) {
                ObjectSpawner.S.IncrementSpeed();
            }
        }

        // Set level and update GUI
        level = levelNdx + 1;
        startingLevel = level;
        UpdateGUI();

        // Set colorNdx
        if (level != 1) {
            ColorManager.S.colorNdx = level - 2;
            ColorManager.S.GetNewColorPalette();
        } else {
            ColorManager.S.ResetPalette();
        }

        // Audio: Confirm
        AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedConfirm);

        // Update main menu UI
        MainMenu.S.UpdateUI();
    }

    // Get and return total time in '00:00:00:000' format
    public string GetTime(float _endingTime) {
        // Get time in seconds
        float time = _endingTime - startingTime;

        // Convert time to '00:00:00:000' format
        TimeSpan t = TimeSpan.FromSeconds(time);
        string timeString = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
        t.Hours,
        t.Minutes,
        t.Seconds,
        t.Milliseconds);

        // Return string
        return timeString;
    }

    //
    public void UpdateGUI() {
        scoreText.text = "Score:<color=white> " + score;
        levelText.text = "Level:<color=white> " + level;
    }
}