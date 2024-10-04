using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip gameMusic;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        // Ensure that the MusicManager is not destroyed between scenes
        DontDestroyOnLoad(gameObject);

        // Add AudioSource component and configure it
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gameMusic;
        audioSource.loop = true;  // Loop the music
        audioSource.playOnAwake = false;  // Don't play immediately
    }



    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();  // Start playing the music
        }
    }
}
