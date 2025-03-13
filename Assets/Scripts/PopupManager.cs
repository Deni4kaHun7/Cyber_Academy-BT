using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private string[] btnsToEnable;
    private GameObject popupBG;

    private Button btnHideIntro;

    private Canvas canvas;
    private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement sceneInfo;

    private void Start()
    {
        popupBG = GameObject.Find("PopupBG");
        
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        uiDocument = GameObject.FindObjectOfType<UIDocument>();
        root = uiDocument.rootVisualElement;      

        btnHideIntro = root.Q<Button>("btnHideIntro"); 

        sceneInfo = root.Q<VisualElement>("SceneInfoContainer");

        btnHideIntro.clicked += () => SwitchPopup("IntroToLevelContainer", true, 1f, false);
    }

    public void SwitchPopup(string elementName, bool btnStatus, float opacityElement, bool visualElementStatus)
    {
        popupBG.SetActive(!popupBG.activeSelf);

        canvas.enabled = !canvas.enabled;

        sceneInfo.style.opacity = opacityElement;

        var elementSwitch = root.Q<VisualElement>(elementName);
        
        if (visualElementStatus) {
            elementSwitch.style.display = DisplayStyle.Flex;
        }
        else
        {
            elementSwitch.style.display = DisplayStyle.None;
        }

        foreach (var btnName in btnsToEnable)
        {
            var button = root.Q<Button>(btnName);
            button.SetEnabled(btnStatus);
            button.style.opacity = opacityElement;
        }
    }
}
