using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; } 
    // Static variable to store the player's score, shared across all instances
    public int score = 0;

    // Static reference to the UI label used to display the score
    private  Label scoreLabel;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Static method to add points to the player's score
    public  void AddScore(int points)
    {
        // Increase the score by the given points, ensuring it doesn't go below zero
        score = Mathf.Max(0, score + points);

        // Update the score label with the new score
        scoreLabel.text = $"Score:{score}";
    }

    public void RegisterLabel(UIDocument uiDocument) {
        // Find the UIDocument in the scene and access the root UI element
        var root = uiDocument.rootVisualElement;

        // Find the label named "scoreLabel" in the UI hierarchy
        scoreLabel = root.Q<Label>("scoreLabel");

        // Set the initial text of the score label
        scoreLabel.text = $"Score:{score}";
    }
}
