using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private VisualElement timePopupContainer;
    private VisualElement timerIntroContainer;
    private Button btnRestart;
    private Button btnStartTimer;
    private float timeRemaining;
    public bool isTimerRunning = false;

    private void Start() 
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        timePopupContainer = root.Q<VisualElement>("TimePopupContainer");
        btnRestart= timePopupContainer.Q<Button>("btnPopup");

        timerIntroContainer = root.Q<VisualElement>("TimerIntroContainer");
        btnStartTimer = root.Q<Button>("btnStartTimer");

        btnRestart.clicked += OnTryAgainBtnClicked;
        btnStartTimer.clicked += AddTimer;

        if(ScoreManager.score == 10) {
            timerIntroContainer.style.display = DisplayStyle.Flex;
        }
    }

    private void Update()
    {
        if(timeRemaining <= 0 && isTimerRunning)
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            timePopupContainer.style.display = DisplayStyle.Flex;
        }
        else if(isTimerRunning)
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
    }

    private void OnTryAgainBtnClicked()
    {
        timeRemaining = timeLimit;
        isTimerRunning = true;
        timePopupContainer.style.display = DisplayStyle.None;
    }

    private void AddTimer()
    {
        timerIntroContainer.style.display = DisplayStyle.None;
        timerLabel.style.display = DisplayStyle.Flex;
        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

     /* private void OnClickIsPhishing()
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
    } */
}
