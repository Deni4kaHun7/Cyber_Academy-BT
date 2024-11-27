using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static int score = 10;
    public static Label scoreLabel;

    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel"); 
        scoreLabel.text = "Score:  " + score.ToString();
    }

    public static void AddScore(int points)
    {
        score = Mathf.Max(0, score + points);
        scoreLabel.text = "Score:  " + score.ToString();
    }
}
