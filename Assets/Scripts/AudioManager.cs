using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource clickAudioSource;
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectsAudioSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip goalAchievedSound;
    [SerializeField] private AudioClip unlockSound;  
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip winnerSound;
    
    [Header("Settings")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float sfxVolume = 0.8f;
    [SerializeField] private float musicVolume = 0.5f;
    
    public static AudioManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayClickSound()
    {
        if (clickSound != null && clickAudioSource != null)
        {
            clickAudioSource.volume = sfxVolume * masterVolume;
            clickAudioSource.PlayOneShot(clickSound);
        }
    }
    
    public void PlayGoalAchievedSound()
    {
        if (goalAchievedSound != null && effectsAudioSource != null)
        {
            effectsAudioSource.volume = sfxVolume * masterVolume;
            effectsAudioSource.PlayOneShot(goalAchievedSound);
        }
    }
    
    public void PlayUnlockSound()
    {
        if (unlockSound != null && effectsAudioSource != null)
        {
            effectsAudioSource.volume = sfxVolume * masterVolume;
            effectsAudioSource.PlayOneShot(unlockSound);
        }
    }
    
    public void PlayGameOverSound()
    {
        if (gameOverSound != null && effectsAudioSource != null)
        {
            effectsAudioSource.volume = sfxVolume * masterVolume;
            effectsAudioSource.PlayOneShot(gameOverSound);
        }
    }
    
    public void PlayWinnerSound()
    {
        if (winnerSound != null && effectsAudioSource != null)
        {
            effectsAudioSource.volume = sfxVolume * masterVolume;
            effectsAudioSource.PlayOneShot(winnerSound);
        }
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
    }
}