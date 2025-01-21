using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
    // Background for the pop-up
    private static GameObject popupBG;

    // Visual element for the introduction pop-up
    private static VisualElement introPopup;
    
    //Visual element for suspicous element counter
    private static VisualElement suspiciousElementsCounter;

    //Visual element for the recap of the lesson's main goal
    private static VisualElement recapGoal;

    // Button to hide the introduction pop-up
    private static Button btnHideIntro;

    // Canvas for the main test UI
    private static Canvas canvas;

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
        suspiciousElementsCounter = root.Q<VisualElement>("SuspiciousElementsCounter");
        recapGoal = root.Q<VisualElement>("RecapGoal");

        // Attach the OnClickHideIntro method to the Hide Intro button's click event
        btnHideIntro.clicked += SwitchPopup;
        btnHideIntro.clicked += OnClickHideIntro;
    }

    // Method called when the "Hide Intro" button is clicked
    public static void SwitchPopup()
    {
        // Toggle the activation state of the pop-up background
        popupBG.SetActive(!popupBG.activeSelf);

        // Toggle the main canvas's enabled state
        canvas.enabled = !canvas.enabled;
    }

    private void OnClickHideIntro()
    {
        // Toggle the display state of the introduction pop-up
        introPopup.style.display = DisplayStyle.None; 
    }

    public static void EnableButtons(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.SetEnabled(true);
            button.style.opacity = 1f;
        }

        // Toggle the visual elements opacity between fully visible and partially transparent
        ScoreManager.scoreLabel.style.opacity = 1f;  
        suspiciousElementsCounter.style.opacity = 1f;
        recapGoal.style.opacity = 1f;
    }

    public static void DisableButtons(params Button[] buttons)
    {
        foreach (var button in buttons)
        {
            button.SetEnabled(false);
            button.style.opacity = 0.07f;
        }

        // Toggle the score label's opacity between fully visible and partially transparent
        ScoreManager.scoreLabel.style.opacity = .07f;  
        suspiciousElementsCounter.style.opacity= .07f;
        recapGoal.style.opacity = .07f;
    }
}
