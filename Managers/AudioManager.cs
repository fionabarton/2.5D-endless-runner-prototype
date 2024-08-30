using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eBGMAudioClipName { mainMenu, optionsMenu, highScoreMenu, gameplay, gameOver, newHighScore, volumeSliderValueChanged };
public enum eSFXAudioClipName { applause, applauseLoop, buttonPressedConfirm, buttonPressedDeny, buttonSelected, buttonPressedPrevious, buttonPressedNext, damage, death, grabCoin, grabCoinLevelUp, grabHanger, grabShield, pause, scream, unpause, volumeSliderValueChanged };
public enum eVOX {
    vox1, vox1ToGo, vox2, vox2ToGo, vox3, vox3ToGo, vox4ToGo, vox5ToGo, voxGameOver, voxGetReady, 
    voxGo, voxLetsGo, voxNewHighScore, voxNextLevel, voxShield, voxNull, volumeSliderValueChanged
};

// Handles playing, stopping, or looping music and sound effects. 
public class AudioManager : MonoBehaviour {
    [Header("Set in Inspector")]
    // BGM audio sources
    public List<AudioSource>    bgmAS = new List<AudioSource>();

    // SFX audio sources
    public AudioSource          applauseAS;
    public AudioSource          applauseLoopAS;
    public AudioSource          buttonPressedPreviousAS;
    public AudioSource          buttonPressedNextAS;
    public AudioSource          buttonPressedConfirmAS;
    public AudioSource          buttonPressedDenyAS;
    public AudioSource          buttonSelectedAS;
    public AudioSource          damageAS;
    public AudioSource          deathAS;
    public AudioSource          grabCoinAS;
    public AudioSource          grabCoinLevelUpAS;
    public AudioSource          grabHangerAS;
    public AudioSource          grabShieldAS;
    public AudioSource          pauseAS;
    public AudioSource          screamAS;
    public AudioSource          unpauseAS;
    public AudioSource          SFXvolumeSliderValueChangedAS;

    public AudioSource          VOXAudioSource;
    public AudioSource          VOXvolumeSliderValueChangedAS;
    public List<AudioClip>      voxClips = new List<AudioClip>();
    public List<AudioClip>      voxExclamationClips = new List<AudioClip>();
    public List<AudioClip>      voxInterjectionClips = new List<AudioClip>();

    [Header("Set Dynamically")]
    public float                previousVolumeLvl;
    public bool                 isMuted;

    public int                  currentSongNdx;

    // Singleton
    private static AudioManager _S;
    public static AudioManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Singleton
        S = this;
    }

    void Start() {
        Invoke("OnStart", 0.11f);
    }

    void OnStart() {
        // Start playing background music
        PlayBGM(eBGMAudioClipName.mainMenu);
    }

    // Plays a specific SFX audio source based on its input, an enum called 'eSFXAudioClipName'
    public void PlaySFX(eSFXAudioClipName clipName) {
        switch (clipName) {
            case eSFXAudioClipName.applause:
                applauseAS.Play();
                break;
            case eSFXAudioClipName.applauseLoop:
                applauseLoopAS.Play();
                break;
            case eSFXAudioClipName.buttonPressedPrevious:
                buttonPressedPreviousAS.Play();
                break;
            case eSFXAudioClipName.buttonPressedNext:
                buttonPressedNextAS.Play();
                break;
            case eSFXAudioClipName.buttonPressedConfirm:
                buttonPressedConfirmAS.Play();
                break;
            case eSFXAudioClipName.buttonPressedDeny:
                buttonPressedDenyAS.Play();
                break;
            case eSFXAudioClipName.buttonSelected:
                buttonSelectedAS.Play();
                break;
            case eSFXAudioClipName.damage:
                damageAS.Play();
                break;
            case eSFXAudioClipName.death:
                deathAS.Play();
                break;
            case eSFXAudioClipName.grabCoin:
                grabCoinAS.Play();
                break;
            case eSFXAudioClipName.grabCoinLevelUp:
                grabCoinLevelUpAS.Play();
                break;
            case eSFXAudioClipName.grabHanger:
                grabHangerAS.Play();
                break;
            case eSFXAudioClipName.grabShield:
                grabShieldAS.Play();
                break;
            case eSFXAudioClipName.pause:
                pauseAS.Play();
                break;
            case eSFXAudioClipName.scream:
                screamAS.Play();
                break;
            case eSFXAudioClipName.unpause:
                unpauseAS.Play();
                break;
            case eSFXAudioClipName.volumeSliderValueChanged:
                SFXvolumeSliderValueChangedAS.Play();
                break;
        }
    }

    // Plays a specific BGM audio source based on its input, an enum called 'eBGMAudioClipName'
    public void PlayBGM(eBGMAudioClipName clipName) {
        // Set current song index
        currentSongNdx = (int)clipName;

        // Stop and reset time for all BGM
        for (int i = 0; i < bgmAS.Count; i++) {
            bgmAS[i].Stop();
            bgmAS[i].time = 0;
        }

        // Play song
        switch (clipName) {
            case eBGMAudioClipName.mainMenu: bgmAS[0].Play(); break;
            case eBGMAudioClipName.optionsMenu: bgmAS[1].Play(); break;
            case eBGMAudioClipName.highScoreMenu: bgmAS[2].Play(); break;
            case eBGMAudioClipName.gameplay: bgmAS[3].Play(); break;
            case eBGMAudioClipName.gameOver: bgmAS[4].Play(); break;
            case eBGMAudioClipName.newHighScore: bgmAS[5].Play(); break;
        }
    }

    public void PlayVOXClip(eVOX VOXName) {
        AudioClip clip = voxClips[(int)VOXName];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
    }

    //
    public void PlayVOXExclamationClip(int ndx) {
        AudioClip clip = voxExclamationClips[ndx];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
    }

    //
    public void PlayVOXInterjectionClip(int ndx) {
        AudioClip clip = voxInterjectionClips[ndx];
        VOXAudioSource.clip = clip;
        VOXAudioSource.Play();
    }

    public void PauseAndMuteAudio() {
        // Pause and mute
        if (!isMuted) {
            previousVolumeLvl = AudioListener.volume;
            AudioListener.volume = 0;
            isMuted = true;

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 0);
            // Unpause and unmute
        } else {
            AudioListener.volume = previousVolumeLvl;
            isMuted = false;

            // Save settings
            PlayerPrefs.SetInt("Mute Audio", 1);
        }
    }

    public void SetMasterVolume(float volume) {
        AudioListener.volume = volume;

        // Set previous volume level
        previousVolumeLvl = volume;

        // Save settings
        PlayerPrefs.SetFloat("Master Volume", volume);

        // Play audio
        PlaySFX(eSFXAudioClipName.buttonPressedConfirm);
    }

    public void SetBGMVolume(float volume) {
        for(int i = 0; i < bgmAS.Count; i++) {
            bgmAS[i].volume = volume;
        }

        // Save settings
        PlayerPrefs.SetFloat("BGM Volume", volume);

        // Play audio
        bgmAS[6].Play();
    }

    public void SetSFXVolume(float volume) {
        applauseAS.volume = volume;
        applauseLoopAS.volume = volume;
        buttonPressedPreviousAS.volume = volume;
        buttonPressedNextAS.volume = volume;
        buttonPressedConfirmAS.volume = volume;
        buttonPressedDenyAS.volume = volume;
        buttonSelectedAS.volume = volume;
        damageAS.volume = volume;
        deathAS.volume = volume;
        grabCoinAS.volume = volume;
        grabCoinLevelUpAS.volume = volume;
        grabHangerAS.volume = volume;
        grabShieldAS.volume = volume;
        pauseAS.volume = volume;
        screamAS.volume = volume;
        unpauseAS.volume = volume;
        SFXvolumeSliderValueChangedAS.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("SFX Volume", volume);

        // Play audio
        PlaySFX(eSFXAudioClipName.volumeSliderValueChanged);
    }

    public void SetVOXVolume(float volume) {
        VOXAudioSource.volume = volume;
        VOXvolumeSliderValueChangedAS.volume = volume;

        // Save settings
        PlayerPrefs.SetFloat("VOX Volume", volume);

        // Play audio
        VOXvolumeSliderValueChangedAS.Play();
    }

    //// Plays a short jingle, then when it's over, resumes playback of the BGM that was playing previously
    //public IEnumerator PlayShortJingleThenResumePreviousBGM(int ndx) {
    //    // Get current BGM's playback time, then stop its playback
    //    float time = bgmCS[currentSongNdx].time;
    //    bgmCS[currentSongNdx].Stop();

    //    // Play the jingle
    //    bgmCS[ndx].Play();

    //    // Get length of the jingle 
    //    AudioClip a = bgmCS[ndx].clip;
    //    float songLength = a.length + 1;

    //    // Wait until jingle is done playing
    //    yield return new WaitForSecondsRealtime(songLength);

    //    // Resume playback of the BGM that was playing previously
    //    bgmCS[currentSongNdx].time = time;
    //    bgmCS[currentSongNdx].Play();
    //}
}