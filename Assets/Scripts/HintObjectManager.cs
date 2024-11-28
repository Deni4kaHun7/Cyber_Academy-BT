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
    private static List<VisualElement> popUpsExplanation;
    private static VisualElement parentPopUp;
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

        popUpsExplanation = root.Query<VisualElement>("Slide").ToList();
        popUpsExplanation.Reverse();
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");

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
                
                parentPopUp.Remove(popUpsExplanation[i]);
                popUpsExplanation.Remove(popUpsExplanation[i]);
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
            popUpsExplanation.Reverse();

            VisualElement firstSlide = popUpsExplanation[0];
            Label firstSlideText = firstSlide.Q<Label>("Label");
            firstSlideText.text = "Good Try! Now lets see what you missed.\n" + firstSlideText.text;
            
            popUpsExplanation[0].style.display = DisplayStyle.Flex;
        }
        parentPopUp.style.display = DisplayStyle.Flex;

        btnFinishTest.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.02f);
        btnFinishTest.SetEnabled(false);
        ScoreManager.scoreLabel.style.color = new Color(219f ,106f ,0f, 0.02f);

        popupBG.SetActive(true);
        canvas.enabled = false;
    }
}
