using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Announces game events (ex. game over, next level, etc.).
public class AnnouncerManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public TMPro.TextMeshProUGUI    announcerText;

    [Header("Set Dynamically")]
    private List<string>            exclamations = new List<string>();
    private List<string>            interjections = new List<string>();

    // Stores the indexes of remaining exclamations/interjections that have yet to be displayed
    private List<int>               remainingExclamationNdxs = new List<int>();
    private List<int>               remainingInterjectionNdxs = new List<int>();

    // Single instance of this class, which provides global acess from other scripts
    private static AnnouncerManager _S;
    public static AnnouncerManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        exclamations = new List<string>() { "Awesome", "Booyah", "Cool", "Cool beans", "Cowabunga", "Dude", "Excellent",
            "Fabulous", "Fantastic", "Far out", "Gee whiz", "Great", "Groovy", "Heck yeah", "Hoorah", "Hooray",
            "Hot diggity dog", "Huzzah", "Incredible", "Nice", "Oh yeah", "Right on", "Splendid", "Sweet", "Terrific",
            "Unreal", "Wahoo", "Whoop dee doo", "Whoopee", "Wicked", "Yahoo", "Yay", "Yippee" };

        interjections = new List<string>() {
            "Blimey", "Blinking heck", "Bloody heck", "Blooming heck", "Dagnabbit", "Dang", "Dang it", "Darn",
            "Darnation", "Dash it", "Doggone it", "Flipping heck", "For crying out loud", "For Heaven's sake",
            "For Pete's sake", "Good gosh", "Goodness me", "Gosh darn it", "Gosh darn it to heck", "Great Scott",
            "Oh fiddlesticks", "Rats", "Shoot" };

        // Populate lists of remaining indexes
        PopulateRemainingExclamationNdxs();
        PopulateRemainingInterjectionNdxs();
    }

    // Functions to announce game events from outside of this script
    public void DisplayRandomExclamation() {
        DisplayText(GetRandomExclamation() + "!", Color.yellow);
    }
    public void DisplayRandomInterjection() {
        DisplayText(GetRandomInterjection() + "!", Color.red);
    }
    public void DisplayShield() {
        DisplayText("SHIELD" + "!", Color.blue);
    }
    public void DisplayNextLevel() {
        DisplayText("NEXT LEVEL" + "!", Color.white);
    }
    public void DisplayGameOver() {
        DisplayText("GAME OVER" + "!", Color.red);
    }
    //public void ResetDisplayText() {
    //    DisplayText("..." + "!", Color.white);
    //}

    // Set text and color of announcer UI text game object
    void DisplayText(string message, Color textColor) {
        announcerText.color = textColor;
        announcerText.text = message;
    }

    // Returns a random positive word or phrase
    string GetRandomExclamation() {
        // If empty, repopulate list of remaining indexes
        if (remainingExclamationNdxs.Count <= 0) {
            PopulateRemainingExclamationNdxs();
        }

        // Get random remaining exclamation index
        int remainingExclamationNdx = UnityEngine.Random.Range(0, remainingExclamationNdxs.Count);

        // Get exclamation index
        int exclamationNdx = remainingExclamationNdxs[remainingExclamationNdx];

        // Remove remaining exclamation from list
        remainingExclamationNdxs.RemoveAt(remainingExclamationNdx);

        // Return exclamation
        return exclamations[exclamationNdx].ToUpper();
    }

    // Returns a random negative word or phrase
    string GetRandomInterjection() {
        // If empty, repopulate list of remaining indexes
        if (remainingInterjectionNdxs.Count <= 0) {
            PopulateRemainingInterjectionNdxs();
        }

        // Get random remaining interjection index
        int remainingInterjectionNdx = UnityEngine.Random.Range(0, remainingInterjectionNdxs.Count);

        // Get interjection index
        int interjectionNdx = remainingInterjectionNdxs[remainingInterjectionNdx];

        // Remove remaining interjection from list
        remainingInterjectionNdxs.RemoveAt(remainingInterjectionNdx);

        // Return interjection
        return interjections[interjectionNdx].ToUpper();
    }

    // Populate lists of indexes of remaining exclamations/interjections that have yet to be displayed
    void PopulateRemainingExclamationNdxs() {
        for (int i = 0; i < exclamations.Count; i++) {
            remainingExclamationNdxs.Add(i);
        }
    }
    void PopulateRemainingInterjectionNdxs() {
        for (int i = 0; i < interjections.Count; i++) {
            remainingInterjectionNdxs.Add(i);
        }
    }

    // Display amount of collectibles needed to proceed to next level
    //void DisplayAmountToNextLevel() {
    //    announcerText.text = ScoreManager.S.amountToNextLevel + "\nTO GO!";
    //}
}