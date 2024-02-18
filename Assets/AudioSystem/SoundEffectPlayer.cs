using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundEffectPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] soundEffects;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlaySoundEffect(int soundEffectIndex) {
        audioSource.clip = soundEffects[soundEffectIndex];
        audioSource.Play();
        StartCoroutine(CompletedSoundEffect(soundEffects[soundEffectIndex].length));
    }

    public void PlaySoundEffect(string soundEffectName) {
        for(int i = 0; i < soundEffects.Length; i++) {
            if(i == soundEffects.Length) {
                Debug.LogWarning("No sound effect found \"" + soundEffectName + "\" in " + this + " sound effect array");
            }else if(soundEffects[i].name == soundEffectName) {
                PlaySoundEffect(i);
                break;
            }
        }
    }

    private IEnumerator CompletedSoundEffect(float audioClipLength) {
        yield return new WaitForSeconds(audioClipLength);
        SendMessageUpwards("SwitchBackToMusic");
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }
}
