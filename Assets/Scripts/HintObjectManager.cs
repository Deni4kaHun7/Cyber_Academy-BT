using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HintObjectManager : MonoBehaviour
{
    private static List<GameObject> hintObjects;
    private static List<VisualElement> popUpsExplanation;
    private static VisualElement parentPopUp;
    private VisualElement introToLevel;
    private Button btnStartLevel;
    private Button btnFinishTest;

    private void Start()
    {
        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));
        GameObject parent = GameObject.Find("ClickContainers");

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        popUpsExplanation = root.Query<VisualElement>("Slide").ToList();
        popUpsExplanation.Reverse();
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");

        introToLevel = root.Q<VisualElement>("IntroToLevelContainer");
        btnStartLevel = root.Q<Button>("btnStartLevel");
        btnStartLevel.clicked += OnClickStartLevel;

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnFinishTest.clicked += OnClickFinishTest;
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
        parentPopUp.style.display = DisplayStyle.Flex;
        popUpsExplanation.Reverse();
        popUpsExplanation[0].style.display = DisplayStyle.Flex;
    }

    private void OnClickStartLevel()
    {
        introToLevel.style.display = DisplayStyle.None;
    }
}
