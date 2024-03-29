using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public enum SongType
{
    StartAnimSong,
    TimeFreezeSong,
    TimeMovingSong,
    HappySong
}

[System.Serializable]
public class SongPair
{
    public SongType Type;
    // public float Volume = 1;
    public AudioClip[] Clips;
}

[RequireComponent(typeof(AudioSource))]

public class SongPlayer : MonoBehaviour
{
    [Tooltip("Add a new enum in the the script for new items")]
    [SerializeField] private SongPair[] songPairs;

    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong(SongType songType, bool repeat)
    {
        SongPair songPair = songPairs.FirstOrDefault(pair => pair.Type == songType);
        audioSource.clip = songPair.Clips[Random.Range(0, songPair.Clips.Length)];
        audioSource.loop = repeat;
        audioSource.Play();
    }

    public void StopSong() {
        audioSource.Stop();
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }

}
