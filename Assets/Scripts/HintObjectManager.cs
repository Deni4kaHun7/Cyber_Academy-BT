using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HintObjectManager : MonoBehaviour
{
    [SerializeField] private bool hasIntro;
    private static List<GameObject> hintObjects;
    private static List<Label> popUpsExplanation;
    private static VisualElement parentPopUp;
    private static VisualElement slidesContainer;
    private VisualElement introToLevel;
    private Button btnFinishTest;
    private Label successMsg;
    private GameObject popupBG;
    private Canvas canvas;

    private void Start()
    {
        popupBG = GameObject.Find("PopupBG");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));
        GameObject parent = GameObject.Find("ClickContainers");

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        slidesContainer = parentPopUp.Q<VisualElement>("Slides");
        popUpsExplanation = slidesContainer.Query<Label>().ToList();

        introToLevel = root.Q<VisualElement>("IntroToLevelContainer");

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnFinishTest.clicked += OnClickFinishTest;

        successMsg = root.Q<Label>("successMsg");
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
        if(popUpsExplanation.Count == 0 ) 
        {
            successMsg.style.display = DisplayStyle.Flex;
        }
        else 
        {
            Label firstSlide = popUpsExplanation[0];
            firstSlide.text = "Good Try! Now lets see what you missed.\n" + firstSlide.text;

            firstSlide.style.display = DisplayStyle.Flex;
        }
        parentPopUp.style.display = DisplayStyle.Flex;

        btnFinishTest.SetEnabled(false);

        popupBG.SetActive(true);
        canvas.enabled = false;
    }
}
