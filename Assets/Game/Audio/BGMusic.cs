using System.Collections;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioClip[] music;
    public float startCooldownMusic;
    private float currentCooldown;
    private AudioSource musicSource;
    private AudioClip _currentMusic;
    private bool canPlay;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        canPlay = true;
        RandomMusic();
        musicSource.PlayOneShot(_currentMusic);
        currentCooldown = startCooldownMusic;
    }
    private void Update()
    {
        if(!musicSource.isPlaying)
        {

            if(canPlay)
            {
                RandomMusic();
                musicSource.PlayOneShot(_currentMusic);
                canPlay = false;
            }
            if(currentCooldown < 0f)
            {
                canPlay = true;
                currentCooldown = startCooldownMusic;
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }
        
    }

    private void RandomMusic()
    {
        _currentMusic = music[Random.Range(0, music.Length - 1)];
        canPlay = false;
    }
    
}
