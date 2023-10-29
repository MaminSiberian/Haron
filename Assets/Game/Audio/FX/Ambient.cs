using System.Data;
using UnityEngine;

public class Ambient : MonoBehaviour
{
    public AudioClip[] FX;
    public float startCooldownMusic;
    private float currentCooldown;
    private AudioSource fXSource;
    private AudioClip _currentFX;
    private bool canPlay;

    private void Start()
    {
        fXSource = GetComponent<AudioSource>();
        canPlay = true;
        RandomFX();
        currentCooldown = startCooldownMusic;
    }

    private void Update()
    {
        if (!fXSource.isPlaying)
        {

            if (canPlay)
            {
                RandomFX();
                fXSource.PlayOneShot(_currentFX);
                canPlay = false;
            }
            if (currentCooldown < 0f)
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

    private void RandomFX()
    {
        _currentFX = FX[Random.Range(0, FX.Length - 1)];
        canPlay = false;
    }

}
