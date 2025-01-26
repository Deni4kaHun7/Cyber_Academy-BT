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
    public static bool isTimerRunning = false;
    private AudioSource audioSource;

    private void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();

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
    }

    private void Update()
    {
        if(timeRemaining <= 0 && isTimerRunning)
        {
            timeRemaining = 0;
            timerLabel.text = "00:00";
            StopTimer();
            failTimerContainer.style.display = DisplayStyle.Flex;
            PopupManager.SwitchPopup();
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

    private void AddTimer()
    {
        timerLabel.style.display = DisplayStyle.Flex;
        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    private void StopTimer()
    {
        isTimerRunning = false;
        timerLabel.style.opacity = .07f;
    }

    private void OnTryAgainBtnClicked()
    {
        timeRemaining = timeLimit;
        isTimerRunning = true;
        failTimerContainer.style.display = DisplayStyle.None;
        PopupManager.SwitchPopup();
        timerLabel.style.opacity = 1f;
    }
}
