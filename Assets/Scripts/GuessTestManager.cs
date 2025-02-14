using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GuessTestManager : MonoBehaviour
{
    [SerializeField] private bool isPhishing;
    [SerializeField] private AudioClip audioClipFail;
    [SerializeField] private AudioClip audioClipWin;
    private VisualElement slidesExplanationContainer;
    private Label[] slidesExplanation;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private AudioSource audioSource;
    private string successMsg;
    private string failMsg;

    private void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        successMsg = "Good Job! Let's take a closer look at this example.\n";
        failMsg = "Wrong! Let's take a closer look at what you missed.\n";

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");
        
        slidesExplanationContainer = root.Q<VisualElement>("SlidesExplanation");
        slidesExplanation = slidesExplanationContainer.Query<Label>().ToList().ToArray();

        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsNotPhishing; 

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");
    }

    private void OnClickIsPhishing()
    {  
        if(isPhishing)
        {
           ShowMessage(successMsg, audioClipWin);
        }
        else
        {
            ShowMessage(failMsg, audioClipFail);
        }
    }

    private void OnClickIsNotPhishing()
    {   
        //PopupManager.EnableExplanationPopup(btnIsPhishing, btnNotPhishing);

        if(isPhishing)
        {
            ShowMessage(successMsg, audioClipFail);
        }
        else
        {
            ShowMessage(failMsg, audioClipWin);
        }
    }

    private void ShowMessage(string msg, AudioClip audioClip)
    {
        ScoreManager.Instance.AddScore();

        Label firstSlide = slidesExplanation[0];
        firstSlide.text = msg + firstSlide.text;

        var PopUpManager = FindObjectOfType<PopupManager>();
        PopUpManager.SwitchPopup("PopUpExplanationContainer", false, 0.07f, true);

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
