using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles creating and adjusting entries on the high score leaderboard.
public class HighScoreManager : MonoBehaviour {
    [Header("Set Dynamically")]
    public HighScore[]  entries;

    public List<string> names = new List<string>();

    [Header("Set Dynamically")]
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
        entries = new HighScore[100];

        //
        SetHighScoresToDefaultValues();
    }

    // Deletes all saved high scores and resets them to default values 
    public void SetHighScoresToDefaultValues(bool saveData = false) {
        // Populate list of high score entries
        int highestScore = 75;
        int highestObjects = 200;
        int highestTime = 360;
        for (int i = 0; i < entries.Length; i++) {
            entries[i] = new HighScore(names[i], highestScore, (int)((highestScore * 0.2f) + 1), highestObjects, ScoreManager.S.GetTime(highestTime));

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