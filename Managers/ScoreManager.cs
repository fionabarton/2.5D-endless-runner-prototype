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
        level = 1;

        UpdateGUI();
    }

    //
    public void UpdateGUI() {
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
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
            // 

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
    }
}