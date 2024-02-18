using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType
{
    Example,
}

[System.Serializable]
public class SFXPair
{
    public SFXType Type;
    public float Volume = 1;
    public AudioClip[] Clips;
}

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

        PlaySoundEffect(SFXType.Example);
    }

    public void PlaySong(int songIndex, bool repeat) {
        musicPlayer.PlaySong(songIndex,repeat);
    }

    private void SwitchBackToMusic() {
        soundEffectPlayer.ChangeVolume(0);
        musicPlayer.ChangeVolume(volume);
    }

    public void PlaySoundEffect(SFXType sfxType) {
        musicPlayer.ChangeVolume(0);
        soundEffectPlayer.ChangeVolume(volume);
        soundEffectPlayer.PlaySoundEffect(sfxType);
    }
}
