using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour {
    public AudioClip punchClip;
    public AudioClip kickClip;

    public AudioSource punchSource;
    public AudioSource kickSource;

    void PlayLightAttackSound() {
        punchSource.clip = punchClip;
        punchSource.Play();
    }

    void PlayHeavyAttackSound() {
        kickSource.clip = kickClip;
        kickSource.Play();
    }
}
