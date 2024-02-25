using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoSingleton<AudioPlayer>
{
    [SerializeField] private bool autoStartMusic = true;
    [SerializeField] private bool loopMusic = true;

    [Range(0, 1)]
    [SerializeField] private float volume = 1;

    private SongPlayer musicPlayer;
    private SFXPlayer sfxPlayer;
    private bool playingWholeSFX = false;

    private void Start()
    {
        musicPlayer = GetComponentInChildren<SongPlayer>();
        sfxPlayer = GetComponentInChildren<SFXPlayer>();
        if (musicPlayer == null) {
            Debug.LogWarning("No Music Player AudioSource childed to " + this);
        }
        if (sfxPlayer == null) {
            Debug.LogWarning("No Sound Effect Player AudioSource childed to " + this);
        }
        musicPlayer.ChangeVolume(volume);
        sfxPlayer.ChangeVolume(0);

        SFXPlayer.CompletedPlayingSFX += SwitchBackToMusic;

        if (autoStartMusic)
        {
            PlaySong(SongType.Placeholder, loopMusic);
        }
    }

    public void PlaySong(SongType songType, bool repeat)
    {
        musicPlayer.PlaySong(songType,repeat);
    }

    private void SwitchBackToMusic()
    {
        playingWholeSFX = false;
        sfxPlayer.ChangeVolume(0);
        musicPlayer.ChangeVolume(volume);
    }

    public void PlayInterruptableSFX(SFXType sfxType)
    {
        if (playingWholeSFX) { return; }

        PlaySFX(sfxType);
    }

    public void PlaySFXAndStopAudio(SFXType sfxType)
    {
        PlayUninterruptableSFX(sfxType);
    }

    public void PlayUninterruptableSFX(SFXType sfxType)
    {
        PlayInterruptableSFX(sfxType);
        playingWholeSFX = true;
    }

    private void PlaySFX(SFXType sfxType)
    {
        musicPlayer.ChangeVolume(0);
        sfxPlayer.ChangeVolume(volume);
        sfxPlayer.PlaySFX(sfxType);
    }
}
