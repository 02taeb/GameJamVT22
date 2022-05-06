using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPuzzle : MonoBehaviour
{
    public string userInput = "Enter here...";
    private GUIStyle uIStyle = new GUIStyle();
    
    private void OnGUI()
    {
        uIStyle.normal.textColor = Color.white;
        uIStyle.fontSize = GameObject.Find("OutputTextField").GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
        uIStyle.alignment = TextAnchor.MiddleCenter;
        userInput = GUI.TextField(new Rect(501, 594, 288, 81), userInput, uIStyle);   
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) 
            && (userInput == "Enter here..." || userInput == "..."))
            userInput = "";
    }
}
