using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    // Singleton instance of ScoreManager, allowing global access to its methods and properties
    public static ScoreManager Instance { get; private set; } 

    // Static variable to store the player's score, shared across all instances
    public int score = 0;

    // Static reference to the Label element used to display the score
    private Label scoreLabel;

    private void Awake() {
        // Check if other instance exists
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        // Set this as the Singleton instance
        Instance = this;

        // Prevent this object from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    // Static method to add points to the player's score
    public void AddScore(int points)
    {
        // Increase the score by the given points, ensuring it doesn't go below zero
        score = Mathf.Max(0, score + points);

        // Update the score Label element with the new score
        scoreLabel.text = $"Score:{score}";
    }

    // Method to initialize the Label element and update its value
    public void RegisterLabel(UIDocument uiDocument) {
        // Find the UIDocument in the scene and access the root UI element
        var root = uiDocument.rootVisualElement;

        // Find the label named "scoreLabel" in the UI hierarchy
        scoreLabel = root.Q<Label>("scoreLabel");

        // Set the initial text of the score label
        scoreLabel.text = $"Score:{score}";
    }
}
