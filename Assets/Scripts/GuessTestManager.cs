using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GuessTestManager : MonoBehaviour
{
    [SerializeField] private bool isPhishing;
    private VisualElement successPopupContainer;
    private VisualElement slidesContainer;
    private Label[] slidesExplanation;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private ScoreManager scoreManager;
    private GameObject popupBG;
    private VisualElement parentPopUp;
    private Canvas canvas;

    private void Start() 
    {
        popupBG = GameObject.Find("PopupBG");
        scoreManager = FindObjectOfType<ScoreManager>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        slidesContainer = root.Q<VisualElement>("Slides");
        slidesExplanation = slidesContainer.Query<Label>().ToList().ToArray();

        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsNotPhishing; 
    }

    private void OnClickIsPhishing()
    {
        DisableBG();

        if(isPhishing)
        {
           ShowSuccessMsg();
            
        }else{
            ShowFailureMsg();
        }
    }

    private void OnClickIsNotPhishing()
    {   
        DisableBG();

        if(isPhishing)
        {
            ShowFailureMsg();
        }else{
            ShowSuccessMsg();
        }
    }

    private void DisableBG()
    {
        parentPopUp.style.display = DisplayStyle.Flex;
        popupBG.SetActive(true);
        btnIsPhishing.SetEnabled(false);
        btnNotPhishing.SetEnabled(false);
        canvas.enabled = false;
        slidesContainer.style.display = DisplayStyle.Flex;
        ScoreManager.scoreLabel.style.color = new Color(219f ,106f ,0f, 0.02f);
    }

    private void ShowSuccessMsg()
    {
        ScoreManager.AddScore(10);
        Label firstSlide = slidesExplanation[0];
        firstSlide.text = "Good Job! Let's take a closer look at this example.\n" + firstSlide.text;
    }

    private void ShowFailureMsg()
    {
        ScoreManager.AddScore(-10);
        Label firstSlide = slidesExplanation[0];
        firstSlide.text = "Wrong! Let's take a closer look at what you missed.\n" + firstSlide.text;
    }
}
