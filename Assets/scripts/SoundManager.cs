using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
    public AudioSource MainMenu;
    public AudioSource Gameplay;
    public AudioSource Scrapping;
    public AudioSource RunePickup;

    public static SoundManager Instance
    {
        get { return _instance; }
    }

    private static SoundManager _instance;

    void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayMainMenuMusic()
    {
        if (Gameplay.isPlaying)
        {
            Gameplay.Stop();
        }

        if (!MainMenu.isPlaying)
        {
            MainMenu.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (MainMenu.isPlaying)
        {
            MainMenu.Stop();
        }

        if (!Gameplay.isPlaying)
        {
            Gameplay.Play();
        }
    }

    public void UpdateScrappingVolume( float volume)
    {
        Scrapping.volume = volume;
    }


    public void PlayScrappingSound(float volume )
    {
        if (!Scrapping.isPlaying)
        {
            Scrapping.volume = volume;
            Scrapping.Play();
        }
    }

    public void StopScrappingSound()
    {
        Scrapping.Stop();
    }

    public void PlayRunePickup()
    {
        RunePickup.Play();
    }

}
