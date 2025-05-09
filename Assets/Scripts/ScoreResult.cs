using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreResult : MonoBehaviour
{
    private Label scoreResult;
    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        // Update the scoreResult Label element with the score value
        scoreResult = root.Q<Label>("scoreLabel");
        scoreResult.text = "Final score: " + ScoreManager.Instance.score;
    }
}
