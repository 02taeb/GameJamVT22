using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PauseMenu : MonoBehaviour
{
    public Slider master, music, video, sfx, ambience;
    public AudioSource musicAS, sfxAS, ambienceAS;
    public VideoPlayer videoPlayer;
    public Text masterT, musicT, videoT, sfxT, ambienceT;
    public GameObject pausePanel;
    private bool wasPlaying;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
            GetPrefs();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            if (PlayerPrefs.HasKey("Master"))
                GetPrefs();
        else 
            SetPrefs();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            if (PlayerPrefs.HasKey("Master"))
                GetPrefs();
            else
            SetPrefs();
    }

    private void OnApplicationQuit()
    {
        SetPrefs();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            PauseComponents();
        }
        SetVolumes();
        SetTexts();
    }

    private void PauseComponents()
    {
        musicAS.Pause();
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            wasPlaying = true;
        }
        if (sfxAS != null)
            sfxAS.Pause();
        ambienceAS.Pause();
    }

    private void ResumeComponents()
    {
        musicAS.UnPause();
        if (wasPlaying)
        {
            videoPlayer.Play();
            wasPlaying = false;
        } 
        if (sfxAS != null)
            sfxAS.UnPause();
        ambienceAS.UnPause();
    }

    private void SetVolumes()
    {
        musicAS.volume = 1 * master.value * music.value;
        videoPlayer.SetDirectAudioVolume(0, 1 * master.value * video.value);
        if (sfxAS != null)
            sfxAS.volume = 1 * master.value * sfx.value;
        ambienceAS.volume = 1 * master.value * ambience.value;
    }

    private void SetTexts()
    {
        masterT.text = Math.Round(master.value * 100, 0).ToString() + "%";
        musicT.text = Math.Round(music.value * 100, 0).ToString() + "%";
        videoT.text = Math.Round(video.value * 100, 0).ToString() + "%";
        sfxT.text = Math.Round(sfx.value * 100, 0).ToString() + "%";
        ambienceT.text = Math.Round(ambience.value * 100, 0).ToString() + "%";
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        if (!wasPlaying)
            Time.timeScale = 1;
        ResumeComponents();
        SetPrefs();
    }

    private void SetPrefs()
    {
        PlayerPrefs.SetFloat("Master", master.value);
        PlayerPrefs.SetFloat("Music", music.value);
        PlayerPrefs.SetFloat("Video", video.value);
        PlayerPrefs.SetFloat("SFX", sfx.value);
        PlayerPrefs.SetFloat("Ambience", ambience.value);
    }

    private void GetPrefs()
    {
        master.value = PlayerPrefs.GetFloat("Master");
        music.value = PlayerPrefs.GetFloat("Music");
        video.value = PlayerPrefs.GetFloat("Video");
        sfx.value = PlayerPrefs.GetFloat("SFX");
        ambience.value = PlayerPrefs.GetFloat("Ambience");
    }

    public void Quit()
    {
        SetPrefs();
        Application.Quit();
    }
}
