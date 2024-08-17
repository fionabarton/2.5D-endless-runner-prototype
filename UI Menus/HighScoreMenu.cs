using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Displays the top 100 highest scoring runs.
public class HighScoreMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Text>   rankText;
    public List<Text>   nameText;
    public List<Text>   scoreText;
    public List<Text>   levelText;
    public List<Text>   objectsText;
    public List<Text>   runTimeText;

    public Button       previousPageButton;
    public Button       nextPageButton;
    public Button       goBackButton;

    public int          currentPageNdx = 0;

    [Header("Set Dynamically")]
    // Single instance of this class, which provides global acess from other scripts
    private static HighScoreMenu _S;
    public static HighScoreMenu S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        // Add listener
        previousPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(-1); });
        nextPageButton.onClick.AddListener(delegate { GoToPreviousOrNextPage(+1); });
        goBackButton.onClick.AddListener(delegate { Deactivate(true); });
        
        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        Deactivate();
    }

    public void ActivateMenu() {
        // Activate this game object
        gameObject.SetActive(true);

        UpdateHighScoreDisplay();

        // Play BGM
        AudioManager.S.PlayBGM(eBGMAudioClipName.highScoreMenu);

        //// Deactivate & remove all listeners from amountButtons
        //for (int i = 0; i < amountButtons.Count; i++) {
        //    amountButtons[i].gameObject.SetActive(false);
        //    amountButtons[i].onClick.RemoveAllListeners();
        //}

        //// Activate selected amountButtons
        //for (int i = 0; i < 20; i++) {
        //    amountButtons[i].gameObject.SetActive(true);
        //}

        //// Set header text
        //headerText.text = "Please select an amount!";

        //for (int i = 0; i < 20; i++) {
        //    // Set button text
        //    amountButtons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();

        //    // Add listeners
        //    int copy = i;
        //    amountButtons[copy].onClick.AddListener(delegate { ScoreManager.S.SetLevel(copy); });
        //    amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
        //}
    }

    void Deactivate(bool playMainMenuBGM = false) {
        if(playMainMenuBGM) {
            AudioManager.S.PlayBGM(eBGMAudioClipName.mainMenu);
        }

        gameObject.SetActive(false);
    }

    public void UpdateHighScoreDisplay(int pageNdx = 0, bool isCalledInStart = true) {
        // Get index of first score to be displayed on this page
        int startingNdx = pageNdx * 10;

        // Loop over the 10 UI text objects
        for (int i = 0; i < 10; i++) {
            // Get rank suffix (1st, 2nd, & 3rd)
            string rankSuffix;
            if (startingNdx + i == 0) {
                rankSuffix = "st";
            } else if (startingNdx + i == 1) {
                rankSuffix = "nd";
            } else if (startingNdx + i == 2) {
                rankSuffix = "rd";
            } else {
                rankSuffix = "th";
            }

            // Set lists of text
            rankText[i].text = (startingNdx + i + 1).ToString() + rankSuffix;
            nameText[i].text = HighScoreManager.S.entries[startingNdx + i].name;
            scoreText[i].text = HighScoreManager.S.entries[startingNdx + i].score.ToString();
            levelText[i].text = HighScoreManager.S.entries[startingNdx + i].level.ToString();
            objectsText[i].text = HighScoreManager.S.entries[startingNdx + i].objects.ToString();
            runTimeText[i].text = HighScoreManager.S.entries[startingNdx + i].runTime;

            // Reset text color
            //SetHighScoreColors(i, "RainbowTextWhite");
        }

        //// 
        //if (isCalledInStart) {
        //    //
        //    if (currentPageNdx == newHighScorePageNdx) {
        //        // Activate cursors 
        //        //GameManager.utilities.SetActiveList(cursorGO, true);

        //        // Set cursor positions
        //        GameManager.utilities.PositionCursor(cursorGO[0], nameText[newHighScoreListNdx].gameObject, -430f, 65, 0);
        //        GameManager.utilities.PositionCursor(cursorGO[1], runTimeText[newHighScoreListNdx].gameObject, 246f, 65, 2);

        //        // Set new HighScore text color to rainbow cycle
        //        SetHighScoreColors(newHighScoreListNdx, "RainbowTextCycle");
        //    } else {
        //        // Deactivate cursors 
        //        GameManager.utilities.SetActiveList(cursorGO, false);
        //    }
        //}

        // Set page text
        //pageText.text = "Page: " + "<color=#D9D9D9>" + (pageNdx + 1).ToString() + "/10" + "</color>";
    }

    // Displays either the previous or next 10 entries of high scores
    public void GoToPreviousOrNextPage(int amountToChange) {
        //
        currentPageNdx += amountToChange;

        // Reset
        if (currentPageNdx > 9) {
            currentPageNdx = 0;
        } else if (currentPageNdx < 0) {
            currentPageNdx = 9;
        }

        UpdateHighScoreDisplay(currentPageNdx);
    }
}