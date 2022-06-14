using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private float sliderVolume;
    public static AudioManager instance;

    public AudioSource Music { get => music; set => music = value; }
    public float SliderVolume { get => sliderVolume; set => sliderVolume = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
