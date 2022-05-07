using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Removes ambiguity in entire file between System.Random and UnityEngine.Random
using Random = UnityEngine.Random;

public class TextPuzzle : MonoBehaviour
{
    public double timeFirstStage, timeSecondStage, timeThirdStage, timeLastStage; 
    public string userInput = "Enter here...";
    public Text timerText;
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
    }

    private void OnGUI()
    {
        uIStyle.normal.textColor = Color.white;
        uIStyle.fontSize = outputText.cachedTextGenerator.fontSizeUsedForBestFit;
        uIStyle.alignment = TextAnchor.MiddleCenter;
        uIStyle.stretchHeight = false;
        uIStyle.stretchWidth = false;
        uIStyle.wordWrap = true;
        uIStyle.clipping = TextClipping.Clip;
        userInput = GUI.TextField(new Rect(484.75f, 595, 320, 80), userInput, CharacterLimit(), uIStyle);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) 
            && (userInput == "Enter here..." || userInput == "..."))
            userInput = "";

        if (active)
        {
            timer -= Time.deltaTime;
            timerText.text = Math.Round(timer, 2).ToString();
            if (userInput == outputText.text)
            {
                timerText.text = "Correct!";
                StartCoroutine(WaitToClear());
                active = false;
                gameController.AffectSpeed(3);
                gameController.AffectDrain(-0.5);
            }
            else if (timer <= 0.0)
            {
                timerText.text = "Time's up!";
                StartCoroutine(WaitToClear());
                active = false;
                gameController.AffectSpeed(-3); // too much?
                gameController.AffectDrain(0.5);
            }
        } 
    }

    public void LoadNextPuzzle()
    {
        DebugAndResetTimer();

        if (counter < puzzles.Length - 1)
            outputText.text = puzzles[counter++];
        else
        {
            Debug.Log("Tried to load next puzzle but has already loaded last puzzle." +
                        "\nPlease enter more puzzles.");
            return;
        }

        active = true;
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
