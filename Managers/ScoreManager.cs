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

    [Header("Set Dynamically")]
    public int                      score = 0;
    public int                      level = 1;
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

    // Reset the user's score to default values
    public void ResetScore() {
        score = 0;
        level = startingLevel;
        startingTime = 0f;
        endingTime = 0f;

        UpdateGUI();
    }

    // Add one point to the user's score and update scoreboard UI
    public void AddToScore() {
        // Increment score and update UI
        score += 1;
        UpdateGUI();

        amountToNextLevel -= 1;

        // Every 5 points, go to next level
        if (score % 5 == 0) {
            // Proceed to next level
            NextLevel();
        } else {
            // Display text of a random exclamation
            AnnouncerManager.S.DisplayRandomExclamation();
        }
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
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
    }
}