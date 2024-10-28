using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerTestManager : MonoBehaviour
{
    [SerializeField] private bool isPhishing;
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private VisualElement timePopupContainer;
    private VisualElement successPopupContainer;
    private VisualElement failPopupContainer;
    private Button btnRestart;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private float timeRemaining;
    private bool isTimerRunning = true;

    private void Start() 
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        var btnContainer = root.Q<VisualElement>("BtnContainer");
        btnIsPhishing = btnContainer.Q<Button>("btnIsPhishing");
        btnNotPhishing = btnContainer.Q<Button>("btnNotPhishing");

        timePopupContainer = root.Q<VisualElement>("TimePopupContainer");
        btnRestart= timePopupContainer.Q<Button>("btnPopup");

        failPopupContainer = root.Q<VisualElement>("FailPopupContainer");
        successPopupContainer = root.Q<VisualElement>("SuccessPopupContainer");

        timeRemaining = timeLimit;

        btnRestart.clicked += OnTryAgainBtnClicked;
        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsPhishing; 
    }

    private void Update()
    {
        if(isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            var minutes = Mathf.FloorToInt(timeRemaining / 60);
            var seconds = Mathf.FloorToInt(timeRemaining % 60);
            if(seconds < 10)
            {
                timerLabel.text = minutes.ToString() + ":0" + seconds;
            }else
            {
                timerLabel.text = minutes.ToString() + ":" + seconds;
            }
        }
        else 
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            timePopupContainer.style.display = DisplayStyle.Flex;
        }
    }

    private void OnTryAgainBtnClicked()
    {
        timeRemaining = timeLimit;
        timePopupContainer.style.display = DisplayStyle.None;
    }

    private void OnClickIsPhishing()
    {
        timeRemaining = timeLimit;
        isTimerRunning = false;
        if(isPhishing)
        {
            successPopupContainer.style.display = DisplayStyle.Flex;
        }else{
            failPopupContainer.style.display = DisplayStyle.Flex;
        }
    }

    private void OnClickIsNotPhishing()
    {
        if(isPhishing)
        {
            failPopupContainer.style.display = DisplayStyle.Flex;
        }else{
            successPopupContainer.style.display = DisplayStyle.Flex;
        }
    }
}
