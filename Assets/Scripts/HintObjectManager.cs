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
            hintObjectDictionary.Add(hintObject, slide);
        }
    }

    public void OnClickHintObject(GameObject currentHintObject)
    {
        var label = hintObjectDictionary[currentHintObject];
        label.RemoveFromHierarchy();
        hintObjectDictionary.Remove(currentHintObject);

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
        if (hintObjectDictionary.Count == 0) 
        {
            successMsg.style.display = DisplayStyle.Flex;
        }
        else 
        {
            Label firstSlide = hintObjectDictionary.First().Value;
            firstSlide.text = "Good Try! Now let's see what you missed.\n" + firstSlide.text;
            firstSlide.style.display = DisplayStyle.Flex;

            SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
        }

        var PopUpManager = FindObjectOfType<PopupManager>();
        PopUpManager.SwitchPopup("PopUpExplanationContainer", false, 0.07f, true);
        
        audioSource.Play();
    }
}
