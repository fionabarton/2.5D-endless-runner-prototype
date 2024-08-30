using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A menu of user-adjustable general options such as audio volume. 
public class OptionsMenu : MonoBehaviour {
    [Header("Set in Inspector")]
    public Slider   masterVolSlider;
    public Slider   BGMVolSlider;
    public Slider   SFXVolSlider;
    public Slider   VOXVolSlider;

    public Button   muteAudioButton;
    public Text     muteAudioButtonText;

    public Button   resetSettingsButton;

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
        BGMVolSlider.onValueChanged.AddListener(delegate { AudioManager.S.SetBGMVolume((BGMVolSlider.value)); });
        SFXVolSlider.onValueChanged.AddListener(delegate { AudioManager.S.SetSFXVolume((SFXVolSlider.value)); });
        VOXVolSlider.onValueChanged.AddListener(delegate { AudioManager.S.SetVOXVolume((VOXVolSlider.value)); });
        goBackButton.onClick.AddListener(delegate { Deactivate(true); });

        // Add listeners to buttons
        muteAudioButton.onClick.AddListener(delegate { MuteAudioButton(); });
        resetSettingsButton.onClick.AddListener(delegate { ResetMenuSettings(); });

        Invoke("OnStart", 0.1f);
    }

    void OnStart() {
        GetPlayerPrefs();

        Deactivate();
    }

    void GetPlayerPrefs() {
        if (PlayerPrefs.HasKey("Master Volume")) {
            masterVolSlider.value = PlayerPrefs.GetFloat("Master Volume");
            AudioManager.S.SetMasterVolume(masterVolSlider.value);
        } else {
            AudioManager.S.SetMasterVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("BGM Volume")) {
            BGMVolSlider.value = PlayerPrefs.GetFloat("BGM Volume");
            AudioManager.S.SetBGMVolume(BGMVolSlider.value);
        } else {
            AudioManager.S.SetBGMVolume(0.45f);
        }

        if (PlayerPrefs.HasKey("SFX Volume")) {
            SFXVolSlider.value = PlayerPrefs.GetFloat("SFX Volume");
            AudioManager.S.SetSFXVolume(SFXVolSlider.value);
        } else {
            AudioManager.S.SetSFXVolume(0.65f);
        }

        if (PlayerPrefs.HasKey("VOX Volume")) {
            VOXVolSlider.value = PlayerPrefs.GetFloat("VOX Volume");
            AudioManager.S.SetVOXVolume(VOXVolSlider.value);
        } else {
            AudioManager.S.SetVOXVolume(0.45f);
        }

        if (PlayerPrefs.HasKey("Mute Audio")) {
            if (PlayerPrefs.GetInt("Mute Audio") == 0) {
                AudioManager.S.PauseAndMuteAudio();
                muteAudioButtonText.text = "Unmute Audio";
            }
        }
    }

    public void ActivateMenu() {
        // Set main menu buttons interactacbility
        MainMenu.S.ButtonsInteractable(false);

        // Set selected game object
        GameManager.S.SetSelectedGO(masterVolSlider.gameObject);

        // Activate this game object
        gameObject.SetActive(true);

        // Audio: Confirm
        AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedConfirm);

        // Play BGM
        AudioManager.S.PlayBGM(eBGMAudioClipName.optionsMenu);
    }

    void Deactivate(bool playMainMenuBGM = false) {
        if (playMainMenuBGM) {
            AudioManager.S.PlayBGM(eBGMAudioClipName.mainMenu);

            // Audio: Confirm
            AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedDeny);
        }

        // Set main menu buttons interactacbility
        MainMenu.S.ButtonsInteractable(true);

        gameObject.SetActive(false);
    }

    // On click (un)mutes all audio
    public void MuteAudioButton() {
        // Delayed text display
        if (!AudioManager.S.isMuted) {
            muteAudioButtonText.text = "Unmute Audio";

            // Play SFX
            AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedPrevious);
        } else {
            muteAudioButtonText.text = "Mute Audio";

            // Play SFX
            AudioManager.S.PlaySFX(eSFXAudioClipName.buttonPressedNext);
        }
        AudioManager.S.PauseAndMuteAudio();
    }

    // Returns all menu settings to their default value
    public void ResetMenuSettings() {
        // Reset slider values
        masterVolSlider.value = 0.5f;
        BGMVolSlider.value = 0.45f;
        SFXVolSlider.value = 0.65f;
        VOXVolSlider.value = 0.45f;

        // Reset mute
        if (AudioManager.S.isMuted) {
        AudioManager.S.PauseAndMuteAudio();
        }
        muteAudioButtonText.text = "Mute Audio";
    }
}