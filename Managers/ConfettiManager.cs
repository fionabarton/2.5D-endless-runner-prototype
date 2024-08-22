using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles starting, stopping, and looping particle systems representing both confetti and fireworks.
public class ConfettiManager : MonoBehaviour {
    [Header("Set in Inspector")]
    public ParticleSystem       confettiPS;
    public ParticleSystem       fireworksPS;

    // Single instance of this class, which provides global acess from other scripts
    private static ConfettiManager _S;
    public static ConfettiManager S { get { return _S; } set { _S = value; } }

    void Awake() {
        // Populate singleton with this instance
        S = this;
    }

    // Plays particle systems that drop confetti for approximately 3 seconds
    public void DropConfetti(bool loopAudio = false, bool playFireworks = false) {
        // Play confetti
        confettiPS.Play();

        // Play SFX
        AudioManager.S.PlaySFX(eSFXAudioClipName.applauseLoop);

        // Play fireworks
        if (playFireworks) {
            fireworksPS.Play();
        }
    }

    // Sets whether all particle systems are looping or not
    public void IsLooping(bool isLooping = false) {
        // Loop confetti
        var confettiMain = confettiPS.main;
        confettiMain.loop = isLooping;
    
        // Loop fireworks
        var fireworksMain = fireworksPS.main;
        fireworksMain.loop = isLooping;

        // Stop looping applause SFX
        if (!isLooping) {
            AudioManager.S.applauseLoopAS.Stop();
        }
    }
}