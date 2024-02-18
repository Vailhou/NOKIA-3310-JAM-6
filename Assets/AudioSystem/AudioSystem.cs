using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    MusicPlayer musicPlayer;
    SoundEffectPlayer soundEffectPlayer;

    [Range(0, 1)]
    public float volume;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponentInChildren<MusicPlayer>();
        soundEffectPlayer = GetComponentInChildren<SoundEffectPlayer>();
        if(musicPlayer == null) {
            Debug.LogWarning("No Music Player AudioSource childed to " + this);
        }
        if(soundEffectPlayer == null) {
            Debug.LogWarning("No Sound Effect Player AudioSource childed to " + this);
        }
        musicPlayer.ChangeVolume(volume);
        soundEffectPlayer.ChangeVolume(0);
    }

    public void PlaySong(int songIndex, bool repeat) {
        musicPlayer.PlaySong(songIndex,repeat);
    }

    private void SwitchBackToMusic() {
        soundEffectPlayer.ChangeVolume(0);
        musicPlayer.ChangeVolume(volume);
    }

    public void PlaySoundEffect(int soundEffectIndex) {
        musicPlayer.ChangeVolume(0);
        soundEffectPlayer.ChangeVolume(volume);
        soundEffectPlayer.PlaySoundEffect(soundEffectIndex);
    }

    public void PlaySoundEffect(string soundEffectName) {
        musicPlayer.ChangeVolume(0);
        soundEffectPlayer.ChangeVolume(volume);
        soundEffectPlayer.PlaySoundEffect(soundEffectName);
    }
}
