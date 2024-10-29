using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObjectManager : MonoBehaviour
{
    //private Dictionary<VisualElement, VisualElement> hintObjectsDict = new Dictionary<VisualElement, VisualElement>();
    private static List<Transform> hintObjects = new List<Transform>();
    private static VisualElement parentPopUp;
    private static VisualElement[] popUpsExplanation;
    public static int score = 0;
    public static Label scoreLabel;

    private void Start()
    {
        GameObject parent = GameObject.Find("ClickContainers");
        foreach(Transform child in parent.transform)
        {
            hintObjects.Add(child);
            Debug.Log(child.name);
        }

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        scoreLabel = root.Q<Label>("scoreLabel");  
        popUpsExplanation = root.Query<VisualElement>("popUp").ToList().ToArray();
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        Debug.Log(popUpsExplanation.Length);
    }

    public static void AddScore(GameObject currentHintObject)
    {
        score ++;
        scoreLabel.text = "Score:  " + score.ToString();
        
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {
            if(hintObjects[i].name == currentHintObject.name)
            {
                Debug.Log(currentHintObject);
                hintObjects.Remove(currentHintObject.transform);
                Destroy(currentHintObject);
                parentPopUp.Remove(popUpsExplanation[i]);
                Debug.Log(popUpsExplanation.Length);
            }
        }
    }

    private void OnClickFinishTest()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        for (int i = hintObjects.Count - 1; i >= 0; i--)
        {

        }
    }
}
