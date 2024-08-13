using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A menu of user-adjustable general options such as audio volume. 
public class OptionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Button   goBackButton;

    [Header("Set Dynamically")]
    // Single instance of this class, which provides global acess from other scripts
    private static OptionsMenu _S;
    public static OptionsMenu S { get { return _S; } set { _S = value; } }

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

    void Deactivate(bool playMainMenuBGM = false) {
        if (playMainMenuBGM) {
            AudioManager.S.PlayBGM(eBGMAudioClipName.mainMenu);
        }

        gameObject.SetActive(false);
    }
}