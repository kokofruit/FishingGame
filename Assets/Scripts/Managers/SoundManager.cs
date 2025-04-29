using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] AudioSource audioSourcePrefab;

    void Awake()
    {
        // Set the singleton instance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Play a sound effect a single time, with adjustable volume
    public void PlaySoundOnce(AudioClip clip, float volume = 1)
    {
        // create new audiosource
        AudioSource player = Instantiate(audioSourcePrefab);
        
        // set clip and volume
        player.clip = clip;
        player.volume = volume;

        // play sound
        player.Play();

        // destroy after done playing
        Destroy(player.gameObject, clip.length);
    }

}
