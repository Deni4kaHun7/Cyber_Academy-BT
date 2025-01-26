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
    private VisualElement successPopupContainer;
    private VisualElement slidesContainer;
    private Label[] slidesExplanation;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private Button btnHideIntro;
    private VisualElement parentPopUp;
    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");
        parentPopUp = root.Q<VisualElement>("PopUpExplanationContainer");
        slidesContainer = root.Q<VisualElement>("SlidesExplanation");
        slidesExplanation = slidesContainer.Query<Label>().ToList().ToArray();
        btnHideIntro = root.Q<Button>("btnHideIntro");

        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsNotPhishing; 

        btnHideIntro.clicked += EnableBtns;

        SlideManager.CreateSlideManager("SlidesIntro", "IntroBtnsContainer");
    }

    private void OnClickIsPhishing()
    {
        DisableBg();

        if(isPhishing)
        {
           ShowSuccessMsg();
        }else{
            ShowFailureMsg();
        }
    }

    private void OnClickIsNotPhishing()
    {   
        DisableBg();

        if(isPhishing)
        {
            ShowFailureMsg();
        }else{
            ShowSuccessMsg();
        }
    }

    private void DisableBg()
    {
        parentPopUp.style.display = DisplayStyle.Flex;

        PopupManager.DisableButtons(btnIsPhishing, btnNotPhishing);
        PopupManager.SwitchPopup();

        SlideManager.CreateSlideManager("SlidesExplanation", "ExplanationBtnsContainer");
        audioSource.Play();
    }

    private void ShowSuccessMsg()
    {
        ScoreManager.Instance.AddScore(10);
        Label firstSlide = slidesExplanation[0];
        firstSlide.text = "Good Job! Let's take a closer look at this example.\n" + firstSlide.text;
        audioSource.clip = audioClipWin;
        audioSource.Play();
    }

    private void ShowFailureMsg()
    {
        ScoreManager.Instance.AddScore(-10);
        Label firstSlide = slidesExplanation[0];
        firstSlide.text = "Wrong! Let's take a closer look at what you missed.\n" + firstSlide.text;
        audioSource.clip = audioClipFail;
        audioSource.Play();
    }

    private void EnableBtns()
    {
        PopupManager.EnableButtons(btnIsPhishing, btnNotPhishing);
    }
}
