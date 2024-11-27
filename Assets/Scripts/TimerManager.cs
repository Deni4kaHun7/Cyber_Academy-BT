using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private VisualElement introToTimerText;
    private VisualElement failTimerText;
    private VisualElement timerIntroContainer;
    private Button btnRestart;
    private Button btnStartTimer;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private float timeRemaining;
    public bool isTimerRunning = false;
    private GameObject popupBG;

    private void Start() 
    {
        popupBG = GameObject.Find("PopupBG");

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        btnRestart= root.Q<Button>("btnRestartTimer");

        timerIntroContainer = root.Q<VisualElement>("IntroToTimerContainer");
        introToTimerText = root.Q<VisualElement>("IntroText");
        failTimerText = root.Q<VisualElement>("FailText");
        btnStartTimer = root.Q<Button>("btnHideIntro");
        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");

        btnRestart.clicked += OnTryAgainBtnClicked;
        btnStartTimer.clicked += AddTimer;

        btnIsPhishing.clicked += StopTimer;
        btnNotPhishing.clicked += StopTimer;
        
        timerIntroContainer.style.display = DisplayStyle.Flex;
    }

    private void Update()
    {
        if(timeRemaining <= 0 && isTimerRunning)
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            timerIntroContainer.style.display = DisplayStyle.Flex;
            introToTimerText.style.display = DisplayStyle.None;
            failTimerText.style.display = DisplayStyle.Flex;
            popupBG.SetActive(true);

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
        timerIntroContainer.style.display = DisplayStyle.None;
        introToTimerText.style.display = DisplayStyle.Flex;
        failTimerText.style.display = DisplayStyle.None;
        popupBG.SetActive(false);
    }

    private void AddTimer()
    {
        timerIntroContainer.style.display = DisplayStyle.None;
        timerLabel.style.display = DisplayStyle.Flex;
        popupBG.SetActive(false);

        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
