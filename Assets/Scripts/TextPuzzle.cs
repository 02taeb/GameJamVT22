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
            // Tänker att speeden på tåget har en konstant drain.
            // Om drainen tar speeden under en viss gräns så Boom!
            // Drainen blir snabbare om man failar tasks
            // och segare om man lyckas.
            // Drainen kanske också passivt ökas lite hela tiden så den inte blir för liten.
            // Kanske beroende på hur snabbt man åker och hur låg drainen är för nuvarande.
            // På det sättet kan det inte gå för bra.
            if (userInput == outputText.text)
            {
                timerText.text = "Correct!"; //Success
                StartCoroutine(WaitToClear());
                active = false;
                //gameController.affectSpeed(int +X);
                //gameController.affectDrain(-0.5); // eller något lämpligt nummer
            }
            else if (timer <= 0.0)
            {
                timerText.text = "Time's up!"; //Fail
                StartCoroutine(WaitToClear());
                active = false;
                //gameController.affectSpeed(int -X); // too much?
                //gameController.affectDrain(0.5); // eller något lämpligt nummer
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
        // if counter < 3, 0-2
        if (counter < firstStage / 2)
        {
            Debug.Log("Time for next letter");
            timer = timeFirstStage;
        }
        // else if counter < 6, 3-5
        else if (counter < firstStage)
        {
            Debug.Log("Time for next letters");
            timer = timeSecondStage;
        }
        // else if counter < 12, 6-11
        else if (counter < secondStage)
        {
            Debug.Log("Time for next word");
            timer = timeThirdStage;
        }
        // else if counter >= 12, 12-?
        else if (counter >= secondStage)
        {
            Debug.Log("Time for next sentence");
            timer = timeLastStage;
        }
    }

    private void IniPuzzles()
    {
        // Comments here and in PopulatePuzzles() if numberOfPuzzles == 20
        puzzles = new string[numberOfPuzzles];
        // firstStage = 6;
        firstStage = numberOfPuzzles / 3;
        // secondStage = 12;
        secondStage = numberOfPuzzles / 3 * 2;
        // lastStage = 20;
        lastStage = numberOfPuzzles;
    }
    
    private void PopulatePuzzles()
    {
        // for 0-2 inclusive
        for (int i = 0; i < firstStage / 2; i++)
        {
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
        }
        // for 3-5
        for (int i = firstStage / 2; i < firstStage; i++)
        {
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
        }
        // for 6-11
        for (int i = firstStage; i < secondStage; i++)
        {
            puzzles[i] = secondStageWords[Random.Range(0, secondStageWords.Length)];
        }
        // for 12-19
        for (int i = secondStage; i < lastStage; i++)
        {
            puzzles[i] = lastStageSentences[Random.Range(0, lastStageSentences.Length)];
        }
    }

    IEnumerator WaitToClear()
    {
        yield return new WaitForSeconds(1);

        outputText.text = "";
        userInput = "...";
    }
}
