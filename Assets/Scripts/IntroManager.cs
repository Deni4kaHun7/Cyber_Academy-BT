using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroManager : MonoBehaviour
{
    // Background for the pop-up
    private GameObject popupBG;

    // Visual element for the introduction pop-up
    private VisualElement introPopup;

    // Button to hide the introduction pop-up
    private Button btnHideIntro;

    // Canvas for the main test UI
    private Canvas canvas;

    // Start is called before the first frame update
    private void Start()
    {
        // Find and assign the pop-up background object from the scene
        popupBG = GameObject.Find("PopupBG");
        
        // Find and assign the main canvas object, and get its Canvas component
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        // Get the UIDocument component and root VisualElement for UI Toolkit elements
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;      

        // Get references to UI Toolkit elements by their names
        btnHideIntro = root.Q<Button>("btnHideIntro"); 
        introPopup = root.Q<VisualElement>("IntroToLevelContainer");

        // Attach the OnClickHideIntro method to the Hide Intro button's click event
        btnHideIntro.clicked += OnClickHideIntro;
    }

    // Method called when the "Hide Intro" button is clicked
    private void OnClickHideIntro()
    {
        // Hide the introduction pop-up
        introPopup.style.display = DisplayStyle.None;

        // Deactivate the pop-up background
        popupBG.SetActive(false);

        // Enable the main canvas to show the test UI
        canvas.enabled = true;

        // Reset the score label's opacity to make it fully visible
        ScoreManager.scoreLabel.style.opacity = 1f;
    }
}
