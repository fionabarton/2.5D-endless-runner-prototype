using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A dynamically populated menu of selectable buttons.
public class NumericalSelectionMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public List<Button>     amountButtons;

    public Button           goBackButton;

    public TextMeshProUGUI  headerText;

    [Header("Set Dynamically")]
    // Single instance of this class, which provides global acess from other scripts
    private static NumericalSelectionMenu _S;
    public static NumericalSelectionMenu S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    void Start() {
        // Add listener
        goBackButton.onClick.AddListener(delegate { Deactivate(); });

        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        Deactivate();
    }

    public void ActivateMenu() {
        // Set main menu buttons interactacbility
        MainMenu.S.ButtonsInteractable(false);

        // Activate this game object
        gameObject.SetActive(true);

        // Deactivate & remove all listeners from amountButtons
        for (int i = 0; i < amountButtons.Count; i++) {
            amountButtons[i].gameObject.SetActive(false);
            amountButtons[i].onClick.RemoveAllListeners();
        }

        // Activate selected amountButtons
        for (int i = 0; i < 20; i++) {
            amountButtons[i].gameObject.SetActive(true);
        }

        // Set selected game object
        GameManager.S.SetSelectedGO(amountButtons[0].gameObject);

        // Audio: Confirm
        AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedConfirm);

        // Set header text
        headerText.text = "Please select an amount!";

        for (int i = 0; i < 20; i++) {
            // Set button text
            amountButtons[i].GetComponentInChildren<Text>().text = (i + 1).ToString();

            // Add listeners
            int copy = i;
            amountButtons[copy].onClick.AddListener(delegate { ScoreManager.S.SetLevel(copy); });
            amountButtons[copy].onClick.AddListener(delegate { Deactivate(); });
        }
    }

    void Deactivate() {
        // Set main menu buttons interactacbility
        MainMenu.S.ButtonsInteractable(true);

        // Audio: Confirm
        AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedDeny);

        gameObject.SetActive(false);
    }
}