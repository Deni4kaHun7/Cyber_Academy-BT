using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuessTestManager : MonoBehaviour
{
    [SerializeField] private bool isPhishing;
    private VisualElement successPopupContainer;
    private VisualElement failPopupContainer;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private TimerManager timerManager;

    private void Start() 
    {
        timerManager = FindObjectOfType<TimerManager>();
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        var btnContainer = root.Q<VisualElement>("BtnContainer");
        btnIsPhishing = btnContainer.Q<Button>("btnIsPhishing");
        btnNotPhishing = btnContainer.Q<Button>("btnNotPhishing");

        failPopupContainer = root.Q<VisualElement>("FailPopupContainer");
        successPopupContainer = root.Q<VisualElement>("SuccessPopupContainer");

        btnIsPhishing.clicked += OnClickIsPhishing;
        btnNotPhishing.clicked += OnClickIsPhishing; 
    }

    private void OnClickIsPhishing()
    {
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
