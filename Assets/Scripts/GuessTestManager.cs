using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GuessTestManager : MonoBehaviour
{
    [SerializeField] private bool isPhishing;
    private VisualElement successPopupContainer;
    private VisualElement failPopupContainer;
    private VisualElement[] slidesExplanation;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private ScoreManager scoreManager;
    private GameObject popupBG;
    private VisualElement parentPopUp;
    private Label successMsg;

    private void Start() 
    {
        popupBG = GameObject.Find("PopupBG");
        scoreManager = FindObjectOfType<ScoreManager>();

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        successMsg = root.Q<Label>("successMsg");
        slidesExplanation = root.Query<VisualElement>("Slide").ToList().ToArray();

        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsNotPhishing; 
    }

    private void OnClickIsPhishing()
    {
        DisableBG();

        if(isPhishing)
        {
            successMsg.style.display = DisplayStyle.Flex;
            ScoreManager.AddScore(10);

            for(int i = slidesExplanation.Length - 1; i>=0; i--)
            {
                slidesExplanation[i].RemoveFromHierarchy();
            }
        }else{
            slidesExplanation[0].style.display = DisplayStyle.Flex;
            ScoreManager.AddScore(-10);
        }
    }

    private void OnClickIsNotPhishing()
    {   
        DisableBG();

        if(isPhishing)
        {
            slidesExplanation[0].style.display = DisplayStyle.Flex;
            ScoreManager.AddScore(-10);
        }else{
            successPopupContainer.style.display = DisplayStyle.Flex;
            ScoreManager.AddScore(10);
        }
    }

    private void DisableBG()
    {
        parentPopUp.style.display = DisplayStyle.Flex;
        popupBG.SetActive(true);
        btnIsPhishing.SetEnabled(false);
        btnNotPhishing.SetEnabled(false);
    }
}
