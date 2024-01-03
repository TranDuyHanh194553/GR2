using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------AudioSource-------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------AudioClip--------")]
    public AudioClip background;//
    public AudioClip clear;//
    public AudioClip fail;//
    public AudioClip bulletShoot;//
    public AudioClip missleShoot;//
    public AudioClip missleCrash;//
    public AudioClip lazerBeam;//
    public AudioClip click;//
    public AudioClip enemySpawn;//
    public AudioClip enemyDie;//
    public AudioClip build;//
    public AudioClip upgrade;
    public AudioClip allySpawn;//
    public AudioClip droneSpawn;
    public AudioClip bulletRecharge;//



    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

