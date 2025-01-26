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
    private static VisualElement suspiciousElementsCounter;
    private static Label suspiciousElementClicked;
    private Label suspiciousElementsTotal;
    private Button btnFinishTest;
    private Button btnHideIntro;
    private Label successMsg;
    private static int suspiciousElementsAmount;
    private AudioSource audioSource;

    private void Start()
    {   
        audioSource = gameObject.GetComponent<AudioSource>();

        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));

        suspiciousElementsAmount = 0;

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        slidesContainer = parentPopUp.Q<VisualElement>("SlidesExplanation");
        suspiciousElementsCounter = root.Q<VisualElement>("SuspiciousElementsCounter");

        popUpsExplanation = slidesContainer.Query<Label>().ToList();
        successMsg = root.Q<Label>("successMsg");
        suspiciousElementClicked = root.Q<Label>("elementsClicked");
        suspiciousElementsTotal = root.Q<Label>("elementsTotal");

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnHideIntro = root.Q<Button>("btnHideIntro");
        
        btnFinishTest.clicked += OnClickFinishTest;
        btnHideIntro.clicked += EnableBtns;

        suspiciousElementsTotal.text = "/" + hintObjects.Count.ToString();

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");
    }

    public static void OnClickHintObject(GameObject currentHintObject)
    {
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {   
            if(hintObjects[i].name == currentHintObject.name)
            {   
                ScoreManager.Instance.AddScore(5);
                
                hintObjects.Remove(currentHintObject);

                BoxCollider2D currentHOboxCollider = currentHintObject.GetComponent<BoxCollider2D>();
                currentHOboxCollider.enabled = false;
                SpriteRenderer currentHOspriteRenderer = currentHintObject.GetComponent<SpriteRenderer>();
                currentHOspriteRenderer.enabled = true;

                suspiciousElementsAmount ++;
                suspiciousElementClicked.text = suspiciousElementsCounter.ToString();
                
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

        PopupManager.DisableButtons(btnFinishTest);

        suspiciousElementsCounter.style.opacity= .07f;

        PopupManager.SwitchPopup();

        // Create a SlideManager to manage slides inside explanation pop-up
        SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
        audioSource.Play();
    }

    private void EnableBtns()
    {
        PopupManager.EnableButtons(btnFinishTest); 
        suspiciousElementsCounter.style.opacity = 1f;
    }
}
