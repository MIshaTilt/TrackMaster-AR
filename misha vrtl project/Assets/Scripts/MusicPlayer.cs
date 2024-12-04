using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI trackTitleText;
    public List<AudioClip> tracks;

    private int currentTrackIndex = 0;
    private bool isPlayerActive = false;

    void Update()
    {
        if (isPlayerActive && !audioSource.isPlaying && !audioSource.loop && audioSource.clip != null)
        {
            PlayNextTrack();
        }
    }

    public void StartPlaying()
    {
        if (tracks.Count > 0)
        {
            isPlayerActive = true;
            PlayTrack(currentTrackIndex);
        }
    }

    void PlayTrack(int index)
    {
        if (index >= 0 && index < tracks.Count)
        {
            
            if (audioSource == null)
            {
                audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
            }
            audioSource.clip = tracks[index];
            audioSource.Play();
            UpdateTrackTitle();
        }
    }

    void UpdateTrackTitle()
    {
        if (trackTitleText != null && audioSource.clip != null)
        {
            trackTitleText.text = audioSource.clip.name;
        }
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        PlayTrack(currentTrackIndex);
    }

    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + tracks.Count) % tracks.Count;
        PlayTrack(currentTrackIndex);
    }

    public void PauseOrResumeTrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    public void UpdateAudio(AudioSource source)
    {
        audioSource = source;
    }
}