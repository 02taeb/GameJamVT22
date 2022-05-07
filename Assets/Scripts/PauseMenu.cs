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
    private GameObject pauseMenu;
    
    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        SetVolumes();
        SetTexts();
    }

    private void SetVolumes()
    {
        musicAS.volume = 1 * master.value * music.value;
        videoPlayer.SetDirectAudioVolume(0, 1 * master.value * video.value);
        sfxAS.volume = 1 * master.value * sfx.value;
        ambienceAS.volume = 1 * master.value * ambience.value;
    }

    private void SetTexts()
    {
        masterT.text = (master.value * 100).ToString() + "%";
        musicT.text = (music.value * 100).ToString() + "%";
        videoT.text = (videoPlayer.GetDirectAudioVolume(0) * 100).ToString() + "%";
        sfxT.text = (sfx.value * 100).ToString() + "%";
        ambienceT.text = (master.value * 100).ToString() + "%";
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Quit()
    {
        PlayerPrefs.SetFloat("Master", master.value);
        PlayerPrefs.SetFloat("Music", music.value);
        PlayerPrefs.SetFloat("Video", video.value);
        PlayerPrefs.SetFloat("SFX", sfx.value);
        PlayerPrefs.SetFloat("Ambience", ambience.value);
        Application.Quit();
    }
}
