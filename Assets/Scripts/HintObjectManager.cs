using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HintObjectManager : MonoBehaviour
{
    private static List<GameObject> hintObjects;
    private static List<Label> popUpsExplanation;
    private static VisualElement parentPopUp;
    private static VisualElement slidesContainer;
    private Button btnFinishTest;
    private Button btnHideIntro;
    private Label successMsg;
    private Label scoreLabel;
    private GameObject popupBG;
    private Canvas canvas;

    private void Start()
    {
        popupBG = GameObject.Find("PopupBG");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        slidesContainer = parentPopUp.Q<VisualElement>("SlidesExplanation");
        popUpsExplanation = slidesContainer.Query<Label>().ToList();
        successMsg = root.Q<Label>("successMsg");
        scoreLabel = root.Query<Label>("scoreLabel");
        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnHideIntro = root.Q<Button>("btnHideIntro");
        
        btnFinishTest.clicked += OnClickFinishTest;
        btnHideIntro.clicked += EnableBtns;

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");
    }

    public static void OnClickHintObject(GameObject currentHintObject)
    {
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {   
            if(hintObjects[i].name == currentHintObject.name)
            {   
                ScoreManager.AddScore(5);
                hintObjects.Remove(currentHintObject);
                Destroy(currentHintObject);
                
                for (int b = popUpsExplanation.Count - 1; b>=0; b--)
                {
                    if (popUpsExplanation[b].name == currentHintObject.name)
                    {
                        popUpsExplanation[b].RemoveFromHierarchy();
                        popUpsExplanation.Remove(popUpsExplanation[b]);
                    }
                }
            }
        }
    }

    private void OnClickFinishTest()
    {   
        // Check if there are any remaining explanation slides (missed suspicious elements).
        if (popUpsExplanation.Count == 0) 
        {
            // If no missed elements, display the success message.
            successMsg.style.display = DisplayStyle.Flex;
        }
        else 
        {
            // If there are missed elements, get the first slide from the list.
            Label firstSlide = popUpsExplanation[0];
            
            // Update the first slide's text to include a message indicating what was missed.
            firstSlide.text = "Good Try! Now let's see what you missed.\n" + firstSlide.text;

            // Display the updated slide.
            firstSlide.style.display = DisplayStyle.Flex;
        }

        // Display the parent container for the explanation pop-up.
        parentPopUp.style.display = DisplayStyle.Flex;

        // Disable the finish test button to prevent multiple clicks.
        btnFinishTest.SetEnabled(false);

        // Set the button's opacity to indicate it is disabled.
        btnFinishTest.style.opacity = 0.07f;

        // Activate the pop-up background to highlight the explanation area.
        popupBG.SetActive(true);

        // Disable the main canvas to hide the test interface.
        canvas.enabled = false;

        // Reduce the opacity of the score label to draw focus to the explanation pop-up.
        scoreLabel.style.opacity = 0.07f;

        // Create a SlideManager to manage slides inside explanation pop-up
        SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
    }

    private void EnableBtns()
    {
        btnFinishTest.SetEnabled(true);
        btnFinishTest.style.opacity = 1f;
    }
}
