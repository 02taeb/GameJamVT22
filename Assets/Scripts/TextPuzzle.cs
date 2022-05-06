using System;
using UnityEngine;
using UnityEngine.UI;

// Removes ambiguity in entire file between System.Random and UnityEngine.Random
using Random = UnityEngine.Random;

public class TextPuzzle : MonoBehaviour
{
    public string userInput = "Enter here...";
    private GUIStyle uIStyle = new GUIStyle();
    private Text outputText;
    [SerializeField]
    private int numberOfPuzzles;
    private int counter = 0;
    private int firstStage;
    private int secondStage;
    private int lastStage;
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
    }

    public void LoadNextPuzzle()
    {
        if (counter < puzzles.Length - 1)
            outputText.text = puzzles[counter++];
        else
            Debug.Log("Tried to load next puzzle but has already loaded last puzzle." +
                        "\nPlease enter more puzzles.");
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
            //puzzles
        }
    }
}
