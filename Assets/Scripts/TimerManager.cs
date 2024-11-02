using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private VisualElement timePopupContainer;
    private Button btnRestart;
    private float timeRemaining;
    public bool isTimerRunning = false;

    private void Start() 
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        timePopupContainer = root.Q<VisualElement>("TimePopupContainer");
        btnRestart= timePopupContainer.Q<Button>("btnPopup");

        timeRemaining = timeLimit;

        btnRestart.clicked += OnTryAgainBtnClicked;
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
        else if(timeLimit > 0)
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
