using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private VisualElement failTimerContainer;
    private Button btnRestart;
    private Button btnStartTimer;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private float timeRemaining;
    public bool isTimerRunning = false;
    private GameObject popupBG;
    private Canvas canvas;

    private void Start() 
    {
        popupBG = GameObject.Find("PopupBG");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        btnRestart= root.Q<Button>("btnRestartTimer");

        failTimerContainer = root.Q<VisualElement>("FailTimerContainer");
        btnStartTimer = root.Q<Button>("btnHideIntro");
        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");

        btnRestart.clicked += OnTryAgainBtnClicked;
        btnStartTimer.clicked += AddTimer;

        btnIsPhishing.clicked += StopTimer;
        btnNotPhishing.clicked += StopTimer;
        
        //timerIntroContainer.style.display = DisplayStyle.Flex;
    }

    private void Update()
    {
        if(timeRemaining <= 0 && isTimerRunning)
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            timerLabel.style.display = DisplayStyle.None;
            failTimerContainer.style.display = DisplayStyle.Flex;
            canvas.enabled = false;
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
        failTimerContainer.style.display = DisplayStyle.None;
        popupBG.SetActive(false);
        canvas.enabled = true;
        timerLabel.style.display = DisplayStyle.Flex;
    }

    private void AddTimer()
    {
        timerLabel.style.display = DisplayStyle.Flex;

        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timerLabel.style.display = DisplayStyle.None;
    }
}
