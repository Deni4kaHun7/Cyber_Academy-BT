using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Array to hold references to buttons named "btnNextScene"
    private Button[] btnNextSceneArray;

    // Unity's Start method, called when the script is initialized
    private void Start()
    {
        // Find the first UIDocument in the scene
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        
        // Get the root element of the UI hierarchy
        var root = uiDocument.rootVisualElement;
        
        // Find all buttons named "btnNextScene" and store them in the array
        btnNextSceneArray = root.Query<Button>("btnNextScene").ToList().ToArray();

        // Assign the OnClickNextScene function to the "clicked" event of each button
        foreach (var btn in btnNextSceneArray)
        {
            btn.clicked += OnClickNextScene;
        }

        ScoreManager.Instance.RegisterLabel(uiDocument);
        
        ButtonSoundManager.Instance.RegisterButtons(uiDocument);
    }

    // Function to load the next scene in the build settings
    private void OnClickNextScene()
    {
        // Get the index of the current scene and increment it by 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Load the scene at the calculated index
        SceneManager.LoadScene(nextSceneIndex);
    }
}

