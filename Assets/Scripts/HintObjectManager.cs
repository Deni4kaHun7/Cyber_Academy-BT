using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;

public class HintObjectManager : MonoBehaviour
{
    private Dictionary<GameObject, Label> hintObjectDictionary;
    private List<GameObject> hintObjects;
    private Label suspiciousElementClicked;
    private Label suspiciousElementsTotal;
    private Button btnFinishTest;
    private Label successMsg;
    private int suspiciousElementsAmount;
    private AudioSource audioSource;

    private void Start()
    {   
        audioSource = gameObject.GetComponent<AudioSource>();

        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));

        suspiciousElementsAmount = 0;

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        successMsg = root.Q<Label>("successMsg");

        suspiciousElementClicked = root.Q<Label>("elementsClicked");
        suspiciousElementsTotal = root.Q<Label>("elementsTotal");

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnFinishTest.clicked += OnClickFinishTest;

        suspiciousElementsTotal.text = "/" + hintObjects.Count.ToString();

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");

        hintObjectDictionary = new Dictionary<GameObject, Label>();

        foreach(var hintObject in hintObjects)
        {
            var slide = root.Q<Label>(hintObject.name);
            Debug.Log(slide.text, hintObject);
            hintObjectDictionary.Add(hintObject, slide);
        }
    }

    public void OnClickHintObject(GameObject currentHintObject)
    {
        // Retrieve the associated slide from the dictionary using the clicked hint object reference
        var explanationSlide = hintObjectDictionary[currentHintObject];
        // Remove the respective slide element from the UI 
        explanationSlide.RemoveFromHierarchy();
        // Remove the clicked hint object from the dictionary
        hintObjectDictionary.Remove(currentHintObject);
        // Update the score
        ScoreManager.Instance.AddScore(5);

        // Disable the box collider and enable the sprite renderer
        BoxCollider2D currentHOboxCollider = currentHintObject.GetComponent<BoxCollider2D>();
        currentHOboxCollider.enabled = false;
        SpriteRenderer currentHOspriteRenderer = currentHintObject.GetComponent<SpriteRenderer>();
        currentHOspriteRenderer.enabled = true;

        suspiciousElementsAmount ++;
        suspiciousElementClicked.text = suspiciousElementsAmount.ToString();
    }

    private void OnClickFinishTest()
    {   
        // Display the success message if no hint objects are left.
        if (hintObjectDictionary.Count == 0) 
        {
            successMsg.style.display = DisplayStyle.Flex;
        }
        else 
        {
            // Retrieve the first slide, update the text value and reveal it
            Label firstSlide = hintObjectDictionary.First().Value;
            firstSlide.text = "Good Try! Now let's see what you missed.\n" + firstSlide.text;
            firstSlide.style.display = DisplayStyle.Flex;

            // Create new SlideManager for the explanation slides
            SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
        }

        // Toggle the popup
        var PopUpManager = FindObjectOfType<PopupManager>();
        PopUpManager.SwitchPopup("PopUpExplanationContainer", false, 0.07f, true);
        
        audioSource.Play();
    }
}
