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

        btnFinishTest = root.Q<Button>("btnFinishTest");
        btnHideIntro = root.Q<Button>("btnHideIntro");
        scoreLabel = root.Q<Label>("scoreLabel"); 
        introPopup = root.Q<VisualElement>("IntroToLevelContainer");

        btnFinishTest.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.02f);
        scoreLabel.style.color = new Color(219f ,106f ,0f, 0.02f);

        introPopup.style.display = DisplayStyle.Flex;

        btnHideIntro.clicked += OnClickHideIntro;
    }

    private void OnClickHideIntro(){
        btnFinishTest.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 1f);
        scoreLabel.style.color = new Color(219f ,106f ,0f, 1f);
        introPopup.style.display = DisplayStyle.None;
        popupBG.SetActive(false);
        canvas.enabled = true;
    }
}
