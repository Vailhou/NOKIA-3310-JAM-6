using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum SFXType
{
    Fire,
    WallDestruction,
    PlayerDeath
}

[System.Serializable]
public class SFXPair
{
    public SFXType Type;
    // public float Volume = 1;
    public AudioClip[] Clips;
}

[RequireComponent(typeof(AudioSource))]

public class SFXPlayer : MonoBehaviour
{
    [Tooltip("Add a new enum in the the script for new items")]
    [SerializeField] private SFXPair[] _sfxPairs;

    private AudioSource _audioSource;

    void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
    }

    public void PlaySoundEffect(SFXType sfxType) {
        SFXPair sfxpair = _sfxPairs.FirstOrDefault(pair => pair.Type == sfxType);
        _audioSource.clip = sfxpair.Clips[Random.Range(0, sfxpair.Clips.Length)];
        _audioSource.Play();
        StartCoroutine(CompletedSoundEffect(_audioSource.clip.length));
    }

    private IEnumerator CompletedSoundEffect(float audioClipLength) {
        yield return new WaitForSeconds(audioClipLength);
        SendMessageUpwards("SwitchBackToMusic");
    }

    public void ChangeVolume(float volume) {
        _audioSource.volume = volume;
    }
}
