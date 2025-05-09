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
        // Switch the popup background status
        popupBG.SetActive(!popupBG.activeSelf);
        // Switch the popup UI status
        canvas.enabled = !canvas.enabled;
        // Update the scene info opacity(score, timer etc.)
        sceneInfo.style.opacity = opacityElement;
        // Visual element to be hidden or revealed
        var elementSwitch = root.Q<VisualElement>(elementName);
        
        if (visualElementStatus) {
            elementSwitch.style.display = DisplayStyle.Flex;
        }
        else
        {
            elementSwitch.style.display = DisplayStyle.None;
        }
        // Toggle the opacity and active status of interactive buttons in the scene
        foreach (var btnName in btnsToEnable)
        {
            var button = root.Q<Button>(btnName);
            button.SetEnabled(btnStatus);
            button.style.opacity = opacityElement;
        }
    }
}
