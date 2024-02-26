using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SongStarter : MonoBehaviour
{
    [SerializeField] SongType songType;
    [SerializeField] bool loop;

    private void Start()
    {
        //AudioPlayer.Instance.PlaySong(songType, loop);

        // this is ugly :(
        FindObjectOfType(typeof(AudioPlayer)).GetComponent<AudioPlayer>().PlaySong(songType, loop);
    }
}
