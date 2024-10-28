using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static Label scoreLabel;

    public static void AddScore()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");  

        score ++;
        
        scoreLabel.text = "Score:  " + score.ToString();
    }
}
