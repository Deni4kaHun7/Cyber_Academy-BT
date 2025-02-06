using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private string[] btnsToEnable;
    // Background for the pop-up
    private GameObject popupBG;

    //Visual element for the recap of the lesson's main goal
    private VisualElement recapGoal;

    // Button to hide the introduction pop-up
    private Button btnHideIntro;

    // Canvas for the main test UI
    private Canvas canvas;
    private VisualElement parentPopUp;
    private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement sceneInfo;

    // Start is called before the first frame update
    private void Start()
    {
        // Find and assign the pop-up background object from the scene
        popupBG = GameObject.Find("PopupBG");
        
        // Find and assign the main canvas object, and get its Canvas component
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        // Get the UIDocument component and root VisualElement for UI Toolkit elements
        uiDocument = GameObject.FindObjectOfType<UIDocument>();
        root = uiDocument.rootVisualElement;      

        // Get references to UI Toolkit elements by their names
        btnHideIntro = root.Q<Button>("btnHideIntro"); 
        recapGoal = root.Q<VisualElement>("RecapGoal");
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");

        sceneInfo = root.Q<VisualElement>("SceneInfoContainer");

        // Attach the OnClickHideIntro method to the Hide Intro button's click event
        btnHideIntro.clicked += () => SwitchPopup("IntroToLevelContainer", true, 1f, false);
    }

    // Method called when the "Hide Intro" button is clicked
    public void SwitchPopup(string elementName, bool btnStatus, float opacityElement, bool visualElementStatus)
    {
        // Toggle the activation state of the pop-up background
        popupBG.SetActive(!popupBG.activeSelf);

        // Toggle the main canvas's enabled state
        canvas.enabled = !canvas.enabled;

        sceneInfo.style.opacity = opacityElement;

        var elementSwitch = root.Q<VisualElement>(elementName);
        
        if (visualElementStatus) {
            elementSwitch.style.display = DisplayStyle.Flex;
        }
        else
        {
            elementSwitch.style.display = DisplayStyle.None;
        }

        foreach (var btnName in btnsToEnable)
        {
            var button = root.Q<Button>(btnName);
            button.SetEnabled(btnStatus);
            button.style.opacity = opacityElement;
        }
    }

   /*  private void OnClickHideIntro()
    {
        // Toggle the display state of the introduction pop-up
        introPopup.style.display = DisplayStyle.None; 
        EnableButtons(btnsToEnable);
    }

    private void EnableButtons(params string[] buttonsName)
    {
        foreach (var btnName in buttonsName)
        {
            var button = root.Q<Button>(btnName);
            button.SetEnabled(true);
            button.style.opacity = 1f;
        }
    }

    public static void EnableExplanationPopup(params Button[] buttons) {
        EnableButtons(buttons);
        recapGoal.style.opacity = .07f;

        SwitchPopup();
        SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
        
        parentPopUp.style.display = DisplayStyle.Flex;
    } */
}
