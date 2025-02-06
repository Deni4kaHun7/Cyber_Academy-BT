using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;

public class HintObjectManager : MonoBehaviour
{
    private Dictionary<GameObject, Label> dictionary;
    private List<GameObject> hintObjects;
    private VisualElement suspiciousElementsCounter;
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

        suspiciousElementsCounter = root.Q<VisualElement>("SuspiciousElementsCounter");
        suspiciousElementClicked = root.Q<Label>("elementsClicked");
        suspiciousElementsTotal = root.Q<Label>("elementsTotal");

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnFinishTest.clicked += OnClickFinishTest;

        suspiciousElementsTotal.text = "/" + hintObjects.Count.ToString();

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");

        dictionary = new Dictionary<GameObject, Label>();

        foreach(var hintObject in hintObjects)
        {
            var slide = root.Q<Label>(hintObject.name);
            dictionary.Add(hintObject, slide);
        }
    }

    public void OnClickHintObject(GameObject currentHintObject)
    {
        var label = dictionary[currentHintObject];
        label.RemoveFromHierarchy();
        dictionary.Remove(currentHintObject);

        ScoreManager.Instance.AddScore(5);

        BoxCollider2D currentHOboxCollider = currentHintObject.GetComponent<BoxCollider2D>();
        currentHOboxCollider.enabled = false;
        SpriteRenderer currentHOspriteRenderer = currentHintObject.GetComponent<SpriteRenderer>();
        currentHOspriteRenderer.enabled = true;

        suspiciousElementsAmount ++;
        suspiciousElementClicked.text = suspiciousElementsAmount.ToString();
    }

    private void OnClickFinishTest()
    {   
        // Check if there are any remaining explanation slides (missed suspicious elements).
        if (dictionary.Count == 0) 
        {
            // If no missed elements, display the success message.
            successMsg.style.display = DisplayStyle.Flex;
        }
        else 
        {
            // If there are missed elements, get the first slide from the list.
            Label firstSlide = dictionary.First().Value;
            
            // Update the first slide's text to include a message indicating what was missed.
            firstSlide.text = "Good Try! Now let's see what you missed.\n" + firstSlide.text;

            // Display the updated slide.
            firstSlide.style.display = DisplayStyle.Flex;
        }

        var PopUpManager = FindObjectOfType<PopupManager>();
        PopUpManager.SwitchPopup("PopUpExplanationContainer", false, 0.07f, true);
        
        audioSource.Play();
    }
}
