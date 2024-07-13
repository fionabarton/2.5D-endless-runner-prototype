using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Caches and displays the user's current score.
public class ScoreManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI    scoreText;

    [Header("Set Dynamically")]
    public int                      score = 0;

    // Single instance of this class, which provides global acess from other scripts
    private static ScoreManager     _S;
    public static ScoreManager      S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    // Add one point to the user's score and update scoreboard UI
    public void AddToScore() {
        score += 1;
        scoreText.text = "Score: " + score;
    }

    // Reset the user's score to 0
    public void ResetScore() {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}