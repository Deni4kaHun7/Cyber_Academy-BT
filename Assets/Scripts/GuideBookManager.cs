using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuideBookManager : MonoBehaviour
{
    private Button btnEmail;
    private Button btnTeachers;
    private Button backBtn;
    private VisualElement emailContainer;
    private VisualElement btnContainer;
    
    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        btnEmail = root.Q<Button>("btnEmails");
        backBtn = root.Q<Button>("btnBck");
        emailContainer = root.Q<VisualElement>("EmailContainer");
        btnContainer = root.Q<VisualElement>("BtnContainer");

        
    }

    private void OnClickBtnEmail()
    {
        btnContainer.style.display = DisplayStyle.None;
        emailContainer.style.display = DisplayStyle.Flex;
    }

    private void OnClickBtnBack()
    {
        btnContainer.style.display = DisplayStyle.Flex;
        emailContainer.style.display = DisplayStyle.None;
    }
}
