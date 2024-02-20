using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoSingleton<AudioSystem>
{
    [SerializeField] private bool _autoStartMusic;
    [SerializeField] private bool _loopMusic;

    [Range(0, 1)]
    [SerializeField] private float volume;

    private MusicPlayer _musicPlayer;
    private SFXPlayer _sfxPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
        _sfxPlayer = GetComponentInChildren<SFXPlayer>();
        if (_musicPlayer == null) {
            Debug.LogWarning("No Music Player AudioSource childed to " + this);
        }
        if (_sfxPlayer == null) {
            Debug.LogWarning("No Sound Effect Player AudioSource childed to " + this);
        }
        _musicPlayer.ChangeVolume(volume);
        _sfxPlayer.ChangeVolume(0);

        if (_autoStartMusic)
        {
            PlaySong(SongType.Placeholder, _loopMusic);
        }
    }

    public void PlaySong(SongType songType, bool repeat) {
        _musicPlayer.PlaySong(songType,repeat);
    }

    private void SwitchBackToMusic() {
        _sfxPlayer.ChangeVolume(0);
        _musicPlayer.ChangeVolume(volume);
    }

    public void PlaySoundEffect(SFXType sfxType) {
        _musicPlayer.ChangeVolume(0);
        _sfxPlayer.ChangeVolume(volume);
        _sfxPlayer.PlaySoundEffect(sfxType);
    }
}
