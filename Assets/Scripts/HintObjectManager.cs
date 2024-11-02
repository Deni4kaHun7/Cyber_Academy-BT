using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObjectManager : MonoBehaviour
{
    //private Dictionary<VisualElement, VisualElement> hintObjectsDict = new Dictionary<VisualElement, VisualElement>();
    private static List<GameObject> hintObjects;
    private static VisualElement parentPopUp;
    private static List<VisualElement> popUpsExplanation;
    public static int score = 0;
    public static Label scoreLabel;

    private void Start()
    {
        hintObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("HintObject"));
        GameObject parent = GameObject.Find("ClickContainers");

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");  

        popUpsExplanation = root.Query<VisualElement>("popUp").ToList();
        popUpsExplanation.Reverse();
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        //Debug.Log("Popups count beginning" + popUpsExplanation.Length);
    }

    public static void AddScore(GameObject currentHintObject)
    {
        //Debug.Log("Name " + currentHintObject.name);
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {
            
            if(hintObjects[i].name == currentHintObject.name)
            {
                Debug.Log(i);
                score ++;
                scoreLabel.text = "Score:  " + score.ToString();
                //Debug.Log(currentHintObject);
                hintObjects.Remove(currentHintObject);
                Destroy(currentHintObject);
                parentPopUp.Remove(popUpsExplanation[i]);
                popUpsExplanation.Remove(popUpsExplanation[i]);
            }
        }
    }

    private void OnClickFinishTest()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        /* for (int i = hintObjects.Count - 1; i >= 0; i--)
        {

        } */
    }
}
