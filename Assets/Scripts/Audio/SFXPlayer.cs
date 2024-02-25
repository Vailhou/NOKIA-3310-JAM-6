using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum SFXType
{
    Fire,
    WallDestruction,
    PlayerDeath,
    BulletHitColliderDefault,
    EnemyDeath
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
    [SerializeField] private SFXPair[] sfxPairs;

    public static event Action CompletedPlayingSFX;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlaySFX(SFXType sfxType) {
        SFXPair sfxpair = sfxPairs.FirstOrDefault(pair => pair.Type == sfxType);
        audioSource.clip = sfxpair.Clips[UnityEngine.Random.Range(0, sfxpair.Clips.Length)];
        audioSource.Play();
        StartCoroutine(CompletedSoundEffect(audioSource.clip.length));
    }

    private IEnumerator CompletedSoundEffect(float audioClipLength) {
        yield return new WaitForSeconds(audioClipLength);
        SendMessageUpwards("SwitchBackToMusic");
        //CompletedPlayingSFX.Invoke();
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }
}
