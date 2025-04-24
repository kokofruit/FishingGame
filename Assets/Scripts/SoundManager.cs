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
        AudioSource player = Instantiate(audioSourcePrefab);

        player.clip = clip;
        player.volume = volume;

        player.Play();

        Destroy(player.gameObject, clip.length);
    }

}
