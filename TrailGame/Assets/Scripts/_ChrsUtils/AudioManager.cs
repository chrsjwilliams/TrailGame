﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Clips { CLICK, MODAL, CONFIRM, DECLINE,CANCEL }

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    struct AudioClipEntry
    {
        public Clips clip;
        public AudioClip audioClip;
    }

    [SerializeField] private List<AudioClipEntry> audioClips;
    public Dictionary<Clips, AudioClip> audioLibrary { get; private set; }
    private AudioSource audioSource;
    private AudioClip audioClip;

    private bool fadeAudio = false;

    private TaskManager tm = new TaskManager();
	// Use this for initialization
	void Awake ()
    {
        audioLibrary = new Dictionary<Clips, AudioClip>();
        audioSource = GetComponent<AudioSource>();
        LoadLibrary();
	}

    private void LoadLibrary()
    {
        foreach (var clip in audioClips)
        {
            audioLibrary.Add(clip.clip, clip.audioClip);
        }
    }

    public void PlayClipVaryPitch(Clips clip)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        PlayClip(clip, 1.0f, pitch);
    }

    public void PlayClip(Clips clip)
    {
        PlayClip(clip, 1.0f, 1.0f);
    }

    public void PlayClip(Clips clip, float volume, float pitch)
    {
        audioClip = audioLibrary[clip];
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void StopClip()
    {
        audioSource.Stop();
    }

    public void FadeAudio()
    {
        fadeAudio = true;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void Update()
    {
        tm.Update();
        if(fadeAudio)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0, Time.deltaTime);
            if(audioSource.volume < 0.01f)
            {
                audioSource.Stop();
                audioSource.Stop();
                audioSource.Stop();

                fadeAudio = false;
            }
        }
    }
}
