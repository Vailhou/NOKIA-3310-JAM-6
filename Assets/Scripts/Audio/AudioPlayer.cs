using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class AudioPlayer : MonoSingleton<AudioPlayer>
{
    [SerializeField] private bool _autoStartMusic = true;
    [SerializeField] private bool _loopMusic = true;

    [Range(0, 1)]
    [SerializeField] private float volume = 1;

    private SongPlayer _musicPlayer;
    private SFXPlayer _sfxPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer = GetComponentInChildren<SongPlayer>();
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

    public void PlaySFX(SFXType sfxType) {
        _musicPlayer.ChangeVolume(0);
        _sfxPlayer.ChangeVolume(volume);
        _sfxPlayer.PlaySoundEffect(sfxType);
    }
}
