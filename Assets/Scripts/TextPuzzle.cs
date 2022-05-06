using System;
using UnityEngine;
using UnityEngine.UI;

// Removes ambiguity in entire file between System.Random and UnityEngine.Random
using Random = UnityEngine.Random;

public class TextPuzzle : MonoBehaviour
{
    public double timeFirstStage, timeSecondStage, timeThirdStage, timeLastStage; 
    public string userInput = "Enter here...";
    private GUIStyle uIStyle = new GUIStyle();
    private Text outputText;
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
        userInput = GUI.TextField(new Rect(501, 594, 288, 81), userInput, uIStyle);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) 
            && (userInput == "Enter here..." || userInput == "..."))
            userInput = "";

        if (active)
        {
            if (timer <= 0.0)
            {
                Debug.Log("Fail!"); //Fail
                active = false;
                //gameController.drain += 0.5; // eller något lämpligt nummer
                // Tänker att speeden på tåget har en konstant drain.
                // Om drainen tar speeden under en viss gräns så Boom!
                // Drainen blir snabbare om man failar tasks
                    // och segare om man lyckas.
                // Drainen kanske också passivt ökas lite hela tiden så den inte blir för liten.
                // Kanske beroende på hur snabbt man åker och hur låg drainen är för nuvarande.
                // På det sättet kan det inte gå för bra.
            }

            if (userInput == outputText.text)
            {
                Debug.Log("Sucess"); //Success
                active = false;
                //gameController.affectSpeed(int X); // gameController.affectSpeed(int) får innehålla någon check.
                //gameController.affectDrain(-0.5); // eller något lämpligt nummer
            }
        } 
    }

    public void LoadNextPuzzle()
    {
        if (counter < puzzles.Length - 1)
            outputText.text = puzzles[counter++];
        else
        {
            Debug.Log("Tried to load next puzzle but has already loaded last puzzle." +
                        "\nPlease enter more puzzles.");
            return;
        }

        active = true;

        DebugAndResetTimer();
    }

    private void DebugAndResetTimer()
    {
        if (counter < firstStage / 2)
        {
            Debug.Log("Time for next letter");
            timer = timeFirstStage;
        }
        else if (counter >= firstStage / 2 && counter < firstStage)
        {
            Debug.Log("Time for next letters");
            timer = timeSecondStage;
        }
        else if (counter >= firstStage && counter < secondStage)
        {
            Debug.Log("Time for next word");
            timer = timeThirdStage;
        }
        else if (counter >= secondStage)
        {
            Debug.Log("Time for next sentence");
            timer = timeLastStage;
        }
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
        {
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
        }
        for (int i = firstStage / 2; i < firstStage; i++)
        {
            puzzles[i] = alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
            puzzles[i] += alphabet[Random.Range(0, alphabet.Length)].ToString();
        }
        for (int i = firstStage; i < secondStage; i++)
        {
            puzzles[i] = secondStageWords[Random.Range(0, secondStageWords.Length)];
        }
        for (int i = secondStage; i < lastStage; i++)
        {
            puzzles[i] = lastStageSentences[Random.Range(0, lastStageSentences.Length)];
        }
    }
}
