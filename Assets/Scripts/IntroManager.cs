using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroManager : MonoBehaviour
{
    private GameObject popupBG;
    private VisualElement introPopup;
    private Button btnFinishTest;
    private Button btnHideIntro;
    private Label scoreLabel;
    private bool isIntroHidden = false;
    private Canvas canvas;
    

    // Start is called before the first frame update
    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement; 

        popupBG = GameObject.Find("PopupBG");
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        //btnFinishTest = root.Q<Button>("btnFinishTest");
        btnHideIntro = root.Q<Button>("btnHideIntro");
        Debug.Log(btnHideIntro);
        scoreLabel = root.Q<Label>("scoreLabel"); 
        introPopup = root.Q<VisualElement>("IntroToLevelContainer");

        introPopup.style.display = DisplayStyle.Flex;

        btnHideIntro.clicked += OnClickHideIntro;
    }

    private void OnClickHideIntro(){
        introPopup.style.display = DisplayStyle.None;
        popupBG.SetActive(false);
        canvas.enabled = true;
        Debug.Log("dsdsd");
    }
}
