using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Removes ambiguity in entire file between System.Random and UnityEngine.Random
using Random = UnityEngine.Random;

public class TextPuzzle : MonoBehaviour
{
    public double timeFirstStage, timeSecondStage, timeThirdStage, timeLastStage; 
    public string userInput = "Enter here...";
    public Text timerText, winMsg;
    public AudioClip beep, winSound;
    public ParticleSystem particles;
    private AudioSource audioSource;
    private Text outputText;
    private GUIStyle uIStyle = new GUIStyle();
    private GameController gameController;
    [SerializeField]
    private int numberOfPuzzles;
    private int counter = 0;
    private int firstStage;
    private int secondStage;
    private int lastStage;
    private double timer;
    private double startTimer;
    private bool active;
    private string[] puzzles;
    // Är det elakt att ha specialtecken som "-", ",", "!" och "'"?
    private readonly string[] secondStageWords = { "Eggs", "Hen", "Hammer", "Hen-Hammer", 
        "Train", "Trolley", "Murderer", "Innocents"/*<-- För svår?*/, "Victims" };
    private readonly string[] lastStageSentences = { $"I, {Environment.UserName}, am stupid!",
        "I can't spell!", "Hen-Hammer is very handsome!" };
    private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    private void Start()
    {
        outputText = GameObject.Find("OutputTextField").GetComponent<Text>();
        IniPuzzles();
        PopulatePuzzles();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audioSource = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        uIStyle.normal.textColor = Color.green;
        uIStyle.fontSize = 31;
        uIStyle.alignment = TextAnchor.MiddleCenter;
        uIStyle.stretchHeight = false;
        uIStyle.stretchWidth = false;
        uIStyle.wordWrap = true;
        uIStyle.clipping = TextClipping.Clip;
        userInput = GUI.TextField(new Rect(Screen.width / 2 - 486.1f / 2, Screen.height / 2 + Screen.height / 25, 486.1f, 43.2656f), userInput, CharacterLimit(), uIStyle);   
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)
            && (userInput == "Enter here..." || userInput == "..."))
                userInput = "";

            if (active)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString().Substring(0, timer.ToString().IndexOf(",") + 3);
                if (userInput == outputText.text)
                {
                    timerText.text = "Correct!";
                    StartCoroutine(WaitToClear());
                    active = false;
                    gameController.AffectSpeed(5);
                    gameController.AffectDrain(-0.1);
                    GUI.FocusControl(null);
                }
                else if (timer <= 0.0)
                {
                    timerText.text = "Time's up!";
                    StartCoroutine(WaitToClear());
                    active = false;
                    gameController.AffectDrain(0.05);
                    GUI.FocusControl(null);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            if (!active)
                startTimer += Time.fixedDeltaTime;
            if (startTimer > 3)
            {
                startTimer = 0;
                LoadNextPuzzle();
            }
        }
    }

    public void LoadNextPuzzle()
    {
        if (!active)
        {
            audioSource.PlayOneShot(beep);
            DebugAndResetTimer();
            StopAllCoroutines();
            userInput = "";

            if (counter < puzzles.Length - 1)
                outputText.text = puzzles[counter++];
            else
            {
                WinMsg();
            }

            active = true;
        }
    }

    private void WinMsg()
    {
        particles.gameObject.SetActive(true);
        particles.Play();
        winMsg.text = "Hen-Hammer gives up!\nYou're too good at running over people.\nYou Win!";
        winMsg.color = Color.green;
        winMsg.gameObject.SetActive(true);
        audioSource.PlayOneShot(winSound);
        Time.timeScale = 0;
        StartCoroutine(ReloadSceneTimer());
    }

    IEnumerator ReloadSceneTimer()
    {
        yield return new WaitForSecondsRealtime(5);

        SceneManager.LoadScene(0);
    }

    private int CharacterLimit()
    {
        int limit = int.MinValue;
        foreach (string s in secondStageWords)
        {
            if (limit < s.Length)
                limit = s.Length;
        }
        foreach (string s in lastStageSentences)
        {
            if (limit < s.Length)
                limit = s.Length;
        }
        return limit;
    }

    private void DebugAndResetTimer()
    {
        if (counter < firstStage / 2)
            timer = timeFirstStage;
        else if (counter < firstStage)
            timer = timeSecondStage;
        else if (counter < secondStage)
            timer = timeThirdStage;
        else if (counter >= secondStage)
            timer = timeLastStage;
    }

    private void IniPuzzles()
    {
        puzzles = new string[numberOfPuzzles];
        firstStage = numberOfPuzzles / 3;
        secondStage = numberOfPuzzles / 3 * 2;
        lastStage = numberOfPuzzles;
    }
    
    private void PopulatePuzzles()
    {
        for (int i = 0; i < firstStage / 2; i++)
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
        for (int i = firstStage / 2; i < firstStage; i++)
        {
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
        }
        for (int i = firstStage; i < secondStage; i++)
            puzzles[i] = secondStageWords[Random.Range(0, secondStageWords.Length)];
        for (int i = secondStage; i < lastStage; i++)
            puzzles[i] = lastStageSentences[Random.Range(0, lastStageSentences.Length)];
    }

    IEnumerator WaitToClear()
    {
        yield return new WaitForSeconds(1);

        outputText.text = "";
        userInput = "...";
    }
}
