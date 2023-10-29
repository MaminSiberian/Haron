using UnityEngine;

public class PlayAnimationSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioClip attack;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Sound()
    {
        _source.PlayOneShot(moveSounds[Random.Range(0, moveSounds.Length - 1)]);
    }

    public void Attack()
    {
        _source.PlayOneShot(attack);
    }
}
