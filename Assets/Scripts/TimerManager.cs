using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float timeLimit;
    private Label timerLabel;
    private Button btnRestart;
    private Button btnStartTimer;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private float timeRemaining;
    private bool isTimerRunning = false;
    private AudioSource audioSource;
    private PopupManager PopupManager;

    private void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        PopupManager = FindObjectOfType<PopupManager>();

        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        timerLabel = root.Q<Label>("timerLabel");

        btnRestart= root.Q<Button>("btnRestartTimer");

        btnStartTimer = root.Q<Button>("btnHideIntro");
        btnIsPhishing = root.Q<Button>("btnIsPhishing");
        btnNotPhishing = root.Q<Button>("btnNotPhishing");

        btnRestart.clicked += OnTryAgainBtnClicked;
        btnStartTimer.clicked += StartTimer;

        btnIsPhishing.clicked += StopTimer;
        btnNotPhishing.clicked += StopTimer;
    }

    private void Update()
    {
        if(timeRemaining <= 0 && isTimerRunning)
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            StopTimer();
            PopupManager.SwitchPopup("FailTimerContainer", false, 0.07f, true);
            audioSource.Play();
        }
        else if(isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;

            var minutes = Mathf.FloorToInt(timeRemaining / 60);
            var seconds = Mathf.FloorToInt(timeRemaining % 60);
            if(seconds < 10)
            {
                timerLabel.text = minutes + ":0" + seconds;
            }else
            {
                timerLabel.text = minutes + ":" + seconds;
            }
        }
    }

    private void StartTimer()
    {
        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log(isTimerRunning);
    }

    private void OnTryAgainBtnClicked()
    {
        StartTimer();
        PopupManager.SwitchPopup("FailTimerContainer", true, 1f, false);
    }
}
