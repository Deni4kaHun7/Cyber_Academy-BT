using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Array to hold references to Button elements named "btnNextScene"
    private Button[] btnNextSceneArray;

    private void Start()
    {
        // Find the UIDocument in the scene
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        // Get the root element of the UI hierarchy
        var root = uiDocument.rootVisualElement;
        
        // Find all Button elements named "btnNextScene" and store them in the array
        btnNextSceneArray = root.Query<Button>("btnNextScene").ToList().ToArray();

        // Assign the OnClickNextScene function to the "clicked" event of each button
        foreach (var btn in btnNextSceneArray)
        {
            btn.clicked += OnClickNextScene;
        }

        // Initialize all buttons in the current scene and add a click sound to each
        ButtonSoundManager.Instance.RegisterButtons(uiDocument);
        
        // Initialize Label element for the score
        if(ScoreManager.Instance != null) {
            ScoreManager.Instance.RegisterLabel(uiDocument);
        }
    }

    // Function to load the next scene 
    private void OnClickNextScene()
    {
        // Get the index of the current scene and increment it by 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        // Load the scene at the calculated index
        SceneManager.LoadScene(nextSceneIndex);
    }
}

