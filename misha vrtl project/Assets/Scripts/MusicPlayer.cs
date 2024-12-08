using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI trackTitleText;
    public List<AudioClip> tracks;
    public Sprite play;
    public Sprite pause;
    public Image field;

    private int currentTrackIndex = 0;
    private bool isPlayerActive = false;

    void Update()
    {
        if (isPlayerActive && !audioSource.isPlaying && !audioSource.loop && audioSource.clip != null)
        {
            PlayNextTrack();
            Debug.Log("1");
        }
    }

    private void OnEnable()
    {
        /*PlayNextTrack();
        PauseOrResumeTrack();*/
    }

    public void StartPlaying()
    {
        if (tracks.Count > 0)
        {
            isPlayerActive = true;
            PlayTrack(currentTrackIndex);
            Debug.Log("2");
        }
    }

    void PlayTrack(int index)
    {
        if (index >= 0 && index < tracks.Count)
        {
            
            if (audioSource == null)
            {
                audioSource = GameObject.Find("My Music").GetComponent<AudioSource>();
                Debug.Log("4");
            }
            audioSource.clip = tracks[index];
            audioSource.Play();
            UpdateTrackTitle();
            Debug.Log("3");
        }
        field.sprite = play;
        Debug.Log("5");
    }

    void UpdateTrackTitle()
    {
        if (trackTitleText != null && audioSource.clip != null)
        {
            trackTitleText.text = audioSource.clip.name;
            Debug.Log("6");
        }
    }

    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        PlayTrack(currentTrackIndex);
        Debug.Log("7");
    }

    public void PlayPreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + tracks.Count) % tracks.Count;
        PlayTrack(currentTrackIndex);
        Debug.Log("8");
    }

    public void PauseOrResumeTrack()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            field.sprite = pause;
            Debug.Log("tryna pause");
            Debug.Log(audioSource);
        }
        else
        {
            audioSource.Play();
            field.sprite = play;
            Debug.Log("tryna play");
            Debug.Log(audioSource);
        }
        Debug.Log(audioSource.isPlaying);
        Debug.Log("9");
    }

    public void UpdateAudio(AudioSource source)
    {
        audioSource = source;
    }
}