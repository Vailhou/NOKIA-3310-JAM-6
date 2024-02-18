using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] songs;

    void Awake() {
        audioSource = GetComponent<AudioSource>();   
    }

    public void PlaySong(int songIndex, bool repeat) {
        audioSource.loop = repeat;
        audioSource.clip = songs[songIndex];
        audioSource.Play();
    }

    public void StopSong() {
        audioSource.Stop();
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }

}
