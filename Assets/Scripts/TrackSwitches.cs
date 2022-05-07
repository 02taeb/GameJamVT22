using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackSwitches : MonoBehaviour
{
    public Image[] imgs;
    public Sprite on, off;
    public AudioClip klick;
    public Text text;
    private AudioSource audioSource;
    private bool lOn = true, rOn;
    private int timer = 0; 
    
    private void Start()
    {
        audioSource = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        timer++;
        if (timer % 600 == 0)
            Flip();
    }

    public void Flip()
    {
        audioSource.PlayOneShot(klick);
        lOn = !lOn;
        rOn = !rOn;
        if (text.text == "Left Track")
            text.text = "Right Track";
        else
            text.text = "Left Track";
        if (lOn)
        {
            imgs[0].sprite = on;
            imgs[1].sprite = off;
        }
        else
        {
            imgs[0].sprite = off;
            imgs[1].sprite = on;
        }
    }
}
