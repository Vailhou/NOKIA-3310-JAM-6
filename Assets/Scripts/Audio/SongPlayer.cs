using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SongType
{
    Placeholder,
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
    [SerializeField] private SongPair[] _songPairs;

    private AudioSource _audioSource;

    void Awake() {
        _audioSource = GetComponent<AudioSource>();   
    }

    public void PlaySong(SongType songType, bool repeat) {
        SongPair songPair = _songPairs.FirstOrDefault(pair => pair.Type == songType);
        _audioSource.loop = repeat;
        _audioSource.clip = songPair.Clips[Random.Range(0, songPair.Clips.Length)];
        _audioSource.Play();
    }

    public void StopSong() {
        _audioSource.Stop();
    }

    public void ChangeVolume(float volume) {
        _audioSource.volume = volume;
    }

}
