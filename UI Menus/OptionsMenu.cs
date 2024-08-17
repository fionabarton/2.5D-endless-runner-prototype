using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A menu of user-adjustable general options such as audio volume. 
public class OptionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Slider   masterVolSlider;

    public Button   muteAudioButton;
    public Text     muteAudioButtonText;
    
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
        // Add listeners to sliders
        masterVolSlider.onValueChanged.AddListener(delegate { AudioManager.S.SetMasterVolume((masterVolSlider.value)); });
        goBackButton.onClick.AddListener(delegate { Deactivate(true); });

        // Add listeners to buttons
        muteAudioButton.onClick.AddListener(delegate { MuteAudioButton(); });

        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        Invoke("GetPlayerPrefs", 0.1f);

        Deactivate();
    }

    void GetPlayerPrefs() {
        if (PlayerPrefs.HasKey("Master Volume")) {
            masterVolSlider.value = PlayerPrefs.GetFloat("Master Volume");
            AudioManager.S.SetMasterVolume(masterVolSlider.value);
        } else {
            AudioManager.S.SetMasterVolume(0.25f);
        }

        if (PlayerPrefs.HasKey("Mute Audio")) {
            if (PlayerPrefs.GetInt("Mute Audio") == 0) {
                AudioManager.S.PauseAndMuteAudio();
                muteAudioButtonText.text = "Unmute Audio";
            }
        }
    }

    public void ActivateMenu() {
        // Activate this game object
        gameObject.SetActive(true);

        // Play BGM
        AudioManager.S.PlayBGM(eBGMAudioClipName.optionsMenu);
    }

    void Deactivate(bool playMainMenuBGM = false) {
        if (playMainMenuBGM) {
            AudioManager.S.PlayBGM(eBGMAudioClipName.mainMenu);
        }

        gameObject.SetActive(false);
    }

    // On click (un)mutes all audio
    public void MuteAudioButton() {
        // Delayed text display
        if (!AudioManager.S.isMuted) {
            muteAudioButtonText.text = "Unmute Audio";

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
        } else {
            muteAudioButtonText.text = "Mute Audio";

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 1);
        }
        AudioManager.S.PauseAndMuteAudio();
    }
}