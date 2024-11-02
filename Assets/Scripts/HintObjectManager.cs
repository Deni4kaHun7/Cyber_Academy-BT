using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HintObjectManager : MonoBehaviour
{
    private static List<GameObject> hintObjects;
    private static VisualElement parentPopUp;
    private static List<VisualElement> popUpsExplanation;
    public static int score = 0;
    public static Label scoreLabel;
    private Button finishTestBtn;
    private Button nextLvlBtn;

    private void Start()
    {
        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));
        GameObject parent = GameObject.Find("ClickContainers");

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");  

        popUpsExplanation = root.Query<VisualElement>("Slide").ToList();
        popUpsExplanation.Reverse();
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");

        finishTestBtn = root.Q<Button>("finishTestBtn");
        finishTestBtn.clicked += OnClickFinishTest;

        nextLvlBtn = root.Q<Button>("nextLvlBtn");
        nextLvlBtn.clicked += OnClickNextLevel;
    }

    public static void AddScore(GameObject currentHintObject)
    {
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {   
            if(hintObjects[i].name == currentHintObject.name)
            {
                score ++;
                scoreLabel.text = "Score:  " + score.ToString();
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

    private void OnClickNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
