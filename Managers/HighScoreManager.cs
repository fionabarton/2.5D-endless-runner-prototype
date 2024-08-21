using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles creating and adjusting entries on the high score leaderboard.
public class HighScoreManager : MonoBehaviour {
    [Header("Set Dynamically")]
    public HighScore[] highScores;

    public List<string> names = new List<string>();

    [Header("Set Dynamically")]
    // Index at which to store a new HighScore in the 'highScores' array
    public int newHighScoreNdx = -1;

    // Single instance of this class, which provides global acess from other scripts
    private static HighScoreManager _S;
    public static HighScoreManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        // Populate list of names for high score entries
        names = new List<string>() { "Bernise", "Mortimer", "Ruth", "Herbert", "Ingrid", "Murray", "Thelma", "Martha", "Humphrey", "Norman",
            "Mavis", "Wilbert", "Doris", "Orville", "Maude", "Harold", "Melvin", "Ethel", "Dilmore", "Gertrude",
            "Harriet", "Otis", "Trudy", "Alfred", "Hilda", "Rutherford", "Gladys", "Sherman", "Mabel", "Deforest",
            "Opal", "Edmund", "Bernadette", "Peyton", "Etta", "Silas", "Louise", "Felix", "Ophelia", "Horace",
            "Marjorie", "Reginald", "Helga", "Mortimer", "Ursula", "Cassius", "Millie", "Homer", "Calliope", "Milo",
            "Caldonia", "Edgar", "Blanche", "Rufus", "Betty", "Llewellyn", "Ramona", "Langston", "Henrietta", "Gilbert",
            "Bertie", "Holden", "Maude", "Ignatius", "Drusilla", "Thaddeus", "Wilma", "Percy", "Fanny", "Atlas",
            "Shirley", "Neville", "Liza", "Brooks", "Posey", "Hugo", "Sibyl", "Archibald", "Glenda", "Stanley",
            "Elaine", "Atticus", "Daphne", "Francis", "Susannah", "Rawlins", "Claribel", "Omar", "Astrid", "Waldo",
            "Lucille", "Cornelius", "Darcy", "Gil", "Sandra", "Quincy", "Lucretia", "Chester", "Margaret", "Ansel",
            "Agatha", "Jasper", "Hester", "Amos", "Hazel", "Rollo", "Agnes", "Demetrius", "Pollyanna", "Tobias" };

        // Initialize array of high scores
        highScores = new HighScore[100];

        //
        SetHighScoresToDefaultValues();
    }

    // Checks if the score is a new high score,
    // then returns at what index it belongs in the highScores array
    public bool CheckForNewHighScore(int score) {
        // Load data
        //GameManager.save.Load();

        for (int i = 0; i < highScores.Length; i++) {
            if (score > highScores[i].score) {
                // Set newHighScoreNdx
                newHighScoreNdx = i;

                // Set currentPageNdx
                if (newHighScoreNdx <= 9) {
                    HighScoreMenu.S.currentPageNdx = 0;
                } else if (newHighScoreNdx >= 89) {
                    HighScoreMenu.S.currentPageNdx = 9;
                } else {
                    int tInt = newHighScoreNdx / 10;
                    HighScoreMenu.S.currentPageNdx = tInt % 10;
                }

                // 
                HighScoreMenu.S.newHighScorePageNdx = HighScoreMenu.S.currentPageNdx;

                // Set newHighScoreListNdx
                HighScoreMenu.S.newHighScoreListNdx = newHighScoreNdx % 10;

                return true;
            }
        }
        return false;
    }

    //
    public void AddNewHighScore(HighScore newScore) {
        // Initialize new array
        HighScore[] tScores = new HighScore[100];

        // Set higher scores that will not change position
        for (int i = 0; i < newHighScoreNdx; i++) {
            tScores[i] = highScores[i];
        }

        // Set new score
        tScores[newHighScoreNdx] = newScore;

        // Set lower scores that will shift position to the right by 1
        for (int i = newHighScoreNdx + 1; i < highScores.Length; i++) {
            tScores[i] = highScores[i - 1];
        }

        // 
        highScores = tScores;

        // Save data
        //GameManager.save.Save();

        //
        HighScoreMenu.S.UpdateHighScoreDisplay(HighScoreMenu.S.currentPageNdx);

        //GameManager.S.selectedHighScoreMenuCS.DisplaySelectedHighScoreEntryData(newHighScoreNdx, true);

        // Reset score for next game
        //GameManager.S.score.ResetScore();
    }

    // Deletes all saved high scores and resets them to default values 
    public void SetHighScoresToDefaultValues(bool saveData = false) {
        // Populate list of high score entries
        int highestScore = 75;
        int highestObjects = 200;
        int highestTime = 360;
        for (int i = 0; i < highScores.Length; i++) {
            highScores[i] = new HighScore(names[i], highestScore, (int)((highestScore * 0.2f) + 1), highestObjects, ScoreManager.S.GetTime(highestTime));

            // Decrement topScore
            if (highestScore > 1) {
                highestScore -= 1;
            }

            // Decrement topObjects
            if (highestObjects > 0) {
                highestObjects -= 2;
            }

            // Decrement topTime
            if (highestTime > 0) {
                highestTime -= 3;
            }
        }

        // Save data
        //if (saveData) {
        //    GameManager.save.Save();

        //    UpdateHighScoreDisplay(currentPageNdx);
        //}
    }
}

public class HighScore {
    public string name;
    public int score;
    public int level;
    public int objects;
    public string runTime;

    public string date;
    public string time;
    public int fallCount;
    public int damageCount;
    public int pauseCount;

    public string startingObjectSpeed;
    public string amountToIncreaseObjectSpeed;
    public string startingSpawnSpeed;
    public string amountToDecreaseSpawnSpeed;

    public string chanceToSpawn0;
    public string chanceToSpawn1;
    public string chanceToSpawn2;
    public string chanceToSpawn3;
    public string chanceToSpawn4;
    public string chanceToSpawn5;
    public string chanceToSpawn6;
    public string chanceToSpawn7;
    public string chanceToSpawn8;
    public string chanceToSpawn9;

    public int objectToSpawn0;
    public int objectToSpawn1;
    public int objectToSpawn2;
    public int objectToSpawn3;
    public int objectToSpawn4;
    public int objectToSpawn5;
    public int objectToSpawn6;
    public int objectToSpawn7;
    public int objectToSpawn8;
    public int objectToSpawn9;

    public HighScore(string _name = "", int _score = 0, int _level = 1, int _objects = 0, string _runTime = "00:00:00:000",
        string _date = "1 January, 2025", string _time = "12:00", int _fallCount = 0, int _damageCount = 0, int _pauseCount = 0,
        string _startingObjectSpeed = "6", string _amountToIncreaseObjectSpeed = "1", string _startingSpawnSpeed = "20", string _amountToDecreaseSpawnSpeed = "3",
        string _chanceToSpawn0 = "20%", string _chanceToSpawn1 = "20%", string _chanceToSpawn2 = "20%", string _chanceToSpawn3 = "30%", string _chanceToSpawn4 = "10%", string _chanceToSpawn5 = "0%", string _chanceToSpawn6 = "0%", string _chanceToSpawn7 = "0%", string _chanceToSpawn8 = "0%", string _chanceToSpawn9 = "0%",
        int _objectToSpawn0 = 7, int _objectToSpawn1 = 0, int _objectToSpawn2 = 20, int _objectToSpawn3 = 47, int _objectToSpawn4 = 48, int _objectToSpawn5 = 50, int _objectToSpawn6 = 50, int _objectToSpawn7 = 50, int _objectToSpawn8 = 50, int _objectToSpawn9 = 50) {
        name = _name;
        score = _score;
        level = _level;
        objects = _objects;
        runTime = _runTime;

        date = _date;
        time = _time + " UTC";
        fallCount = _fallCount;
        damageCount = _damageCount;
        pauseCount = _pauseCount;

        startingObjectSpeed = _startingObjectSpeed;
        amountToIncreaseObjectSpeed = _amountToIncreaseObjectSpeed;
        startingSpawnSpeed = _startingSpawnSpeed;
        amountToDecreaseSpawnSpeed = _amountToDecreaseSpawnSpeed;

        chanceToSpawn0 = _chanceToSpawn0;
        chanceToSpawn1 = _chanceToSpawn1;
        chanceToSpawn2 = _chanceToSpawn2;
        chanceToSpawn3 = _chanceToSpawn3;
        chanceToSpawn4 = _chanceToSpawn4;
        chanceToSpawn5 = _chanceToSpawn5;
        chanceToSpawn6 = _chanceToSpawn6;
        chanceToSpawn7 = _chanceToSpawn7;
        chanceToSpawn8 = _chanceToSpawn8;
        chanceToSpawn9 = _chanceToSpawn9;

        objectToSpawn0 = _objectToSpawn0;
        objectToSpawn1 = _objectToSpawn1;
        objectToSpawn2 = _objectToSpawn2;
        objectToSpawn3 = _objectToSpawn3;
        objectToSpawn4 = _objectToSpawn4;
        objectToSpawn5 = _objectToSpawn5;
        objectToSpawn6 = _objectToSpawn6;
        objectToSpawn7 = _objectToSpawn7;
        objectToSpawn8 = _objectToSpawn8;
        objectToSpawn9 = _objectToSpawn9;
    }
}