using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonSoundManager : MonoBehaviour
{
    // Singleton instance of ButtonSoundManager, allowing global access to its methods and properties
    public static ButtonSoundManager Instance { get; private set;}
    // Holds the reference to the audio source 
    private AudioSource audioSource;

    private void Awake()
    {
        // Check if other instance exists
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        // Set this as the Singleton instance
        Instance = this;
        // Prevent this object from being destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
    
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    
    public void RegisterButtons(UIDocument uiDocument) {
        // Initialize all the buttons in the current scene
        var root = uiDocument.rootVisualElement;
        var buttons = root.Query<Button>().ToList();

        // Assign a method to play the sound of the click event
        foreach (var btn in buttons)
        {
            btn.clicked += () => audioSource.Play();
        }
    }
}
