using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RedGreenButtonPuzzle : MonoBehaviour
{
    public AudioClip doubleBeep;
    private AudioSource audioSource;
    private GameController gameController;
    private bool isButtonOf;
    private float timerBetweenTurnOffs;
    [SerializeField] private float startTimer = 5f;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    [SerializeField] private Button button5;
    [SerializeField] private Button button6;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;
    [SerializeField] private Image image4;
    [SerializeField] private Image image5;
    [SerializeField] private Image image6;
    [SerializeField] private float timeToFail = 10f;

    // Start is called before the first frame update
    void Start()
    {
        timerBetweenTurnOffs = startTimer;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        image1.enabled = true;
        image2.enabled = true;
        image3.enabled = true;
        image4.enabled = true;
        image5.enabled = true;
        image6.enabled = true;
        isButtonOf = false;

        button1.onClick.AddListener(turnButton1On);
        button2.onClick.AddListener(turnButton2On);
        button3.onClick.AddListener(turnButton3On);
        button4.onClick.AddListener(turnButton4On);
        button5.onClick.AddListener(turnButton5On);
        button6.onClick.AddListener(turnButton6On);
    }

    // Update is called once per frame
    void Update()
    {
        turnButtonsOff();
        timeToDeath();
    }

    public void turnButtonsOff()
    {
        timerBetweenTurnOffs -= Time.deltaTime;

        if(timerBetweenTurnOffs <= 0)
        {
            int rnd = Random.Range(0, 6);
            audioSource.PlayOneShot(doubleBeep);
            switch (rnd)
            {
                case 0:
                    image1.enabled=false;
                    break;
                case 1:
                    image2.enabled=false;
                    break;
                case 2:
                    image3.enabled = false;
                    break;
                case 3:
                    image4.enabled = false;
                    break;
                case 4:
                    image5.enabled = false;
                    break;
                case 5:
                    image6.enabled = false;
                    break;
                default:
                    break;
            }
            isButtonOf = true;
            timerBetweenTurnOffs = startTimer;
        }

    }

    private void timeToDeath()
    {
        

        if (isButtonOf)
        {
            timeToFail += Time.deltaTime;
            if (timeToFail >= 10)
            {
                Debug.Log("DamageFromRedGreenPuzzle");
                gameController.AffectSpeed(-3); // too much?
                gameController.AffectDrain(0.05);
                timeToFail = 0;
            }
        }
    }

    private void turnButton1On()
    {
        image1.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }
    private void turnButton2On()
    {
        image2.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }
    private void turnButton3On()
    {
        image3.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }
    private void turnButton4On()
    {
        image4.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }
    private void turnButton5On()
    {
        image5.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }
    private void turnButton6On()
    {
        image6.enabled = true;
        isButtonOf = false;
        timeToFail = 0;
        Success();
    }

    private void Success()
    {
        gameController.AffectSpeed(3);
        gameController.AffectDrain(-0.1);
    }
}
