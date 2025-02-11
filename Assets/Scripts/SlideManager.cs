using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    private string slideContainerName;
    private string btnsContainerName;
    private int currentSlideIndex = 0;
    private Button btnNextSlide;
    private Button btnPrevSlide;
    private Button btnHideIntro;
    private Label[] slidesArray;
    private VisualElement btnsContainer;
    private VisualElement slideParentContainer;
    private bool isUpdateEnabled = true;

    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slideParentContainer = root.Q<VisualElement>(slideContainerName);
        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();

        if(slidesArray.Length < 2)
        {
            isUpdateEnabled = false;
        }

        btnsContainer = root.Q<VisualElement>(btnsContainerName);
        btnNextSlide = btnsContainer.Query<Button>("btnNextSlide");
        btnNextSlide.clicked += OnClickNextSlide;
        btnNextSlide.SetEnabled(false);
        
        btnPrevSlide = btnsContainer.Q<Button>("btnPrevSlide");
        btnPrevSlide.clicked += OnClickPrevSlide;
        btnPrevSlide.SetEnabled(false);

        btnHideIntro = root.Q<Button>("btnHideIntro");
        btnHideIntro.SetEnabled(false);
    }

    private void Update()
    {
        if(!isUpdateEnabled)
        {
            return;
        } 

        if(currentSlideIndex != 0)
        {
            btnPrevSlide.style.opacity = 1f;
            btnPrevSlide.SetEnabled(true);
        }
        else if (currentSlideIndex == 0)
        {
            btnPrevSlide.style.opacity = .07f;
            btnPrevSlide.SetEnabled(false);
        }

        if (currentSlideIndex == slidesArray.Length - 1) 
        {
            btnNextSlide.style.opacity = .07f;
            btnNextSlide.SetEnabled(false);

            btnHideIntro.style.opacity = 1f;
            btnHideIntro.SetEnabled(true);
        }
        else 
        {
            btnNextSlide.style.opacity = 1f;
            btnNextSlide.SetEnabled(true);
        }
    }

    private void ShowSlide(int index)
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        slidesArray[index].style.display = DisplayStyle.Flex;
    }

    private void OnClickNextSlide()
    {
        currentSlideIndex ++;
        ShowSlide(currentSlideIndex);
    }

    private void OnClickPrevSlide()
    {   
        currentSlideIndex --;
        ShowSlide(currentSlideIndex);
    }

    public static void CreateSlideManager(string slideName, string btnContainer)
    {
        GameObject slidesIntroManager = new GameObject("SlidesManager");
        SlideManager slideManagerScript = slidesIntroManager.AddComponent<SlideManager>();
        slideManagerScript.slideContainerName = slideName;
        slideManagerScript.btnsContainerName = btnContainer;
    } 
}
