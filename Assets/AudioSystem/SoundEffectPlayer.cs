using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(AudioSource))]

public class SoundEffectPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private SFXPair[] _sfxPairs;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    public void PlaySoundEffect(SFXType sfxType) {
        SFXPair sfxpair = _sfxPairs.FirstOrDefault(pair => pair.Type == sfxType);
        audioSource.clip = sfxpair.Clips[Random.Range(0, sfxpair.Clips.Length)];
        audioSource.Play();
        StartCoroutine(CompletedSoundEffect(audioSource.clip.length));
    }

    private IEnumerator CompletedSoundEffect(float audioClipLength) {
        yield return new WaitForSeconds(audioClipLength);
        SendMessageUpwards("SwitchBackToMusic");
    }

    public void ChangeVolume(float volume) {
        audioSource.volume = volume;
    }
}
