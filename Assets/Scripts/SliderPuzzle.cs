using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPuzzle : MonoBehaviour
{
    public Slider[] sliders;
    public AudioClip error;
    private AudioSource audioSource;
    private float[] values = new float[3];
    private GameController gameController;
    private bool change;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            int i = 0;
            foreach (Slider slider in sliders)
            {
                values[i] = slider.value;
                slider.value -= 0.0001f;
                if (slider.value <= 0)
                {
                    audioSource.PlayOneShot(error);
                    slider.value = 0.5f;
                    gameController.AffectDrain(0.05);
                    gameController.AffectSpeed(-3);
                }
                i++;
            }
        }
    }

    public void OnValueChangedOne()
    {
        if (sliders[0].value >= 1 && !change)
        {
            change = true;
            gameController.AffectDrain(-0.1);
            StartCoroutine(Wait());
        }
    }

    public void OnValueChangedTwo()
    {
        if (sliders[1].value >= 1 && !change)
        {
            change = true;
            gameController.AffectDrain(-0.1);
            StartCoroutine(Wait());
        }
    }

    public void OnValueChangedThree()
    {
        if (sliders[2].value >= 1 && !change)
        {
            change = true;
            gameController.AffectDrain(-0.1);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);

        change = false;
    }
}
