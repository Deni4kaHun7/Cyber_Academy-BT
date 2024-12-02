using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    public string slideContainerName;
    public string btnsContainerName;
    private int currentSlideIndex = 0;
    private Button btnNextSlide;
    private Button btnPrevSlide;
    private Button btnFinishTest;
    private Button btnIsPhishing;
    private Button btnNotPhishing;
    private Label[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;
    private VisualElement btnsContainer;
    private VisualElement slideParentContainer;
    private bool isUpdateEnabled = true;

    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slideParentContainer = root.Q<VisualElement>(slideContainerName);
        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();

        btnsContainer = root.Q<VisualElement>(btnsContainerName);
        btnNextSlide = btnsContainer.Query<Button>("btnNextSlide");
        btnNextSlide.clicked += OnClickNextSlide;
        
        btnPrevSlide = btnsContainer.Q<Button>("btnPrevSlide");
        btnPrevSlide.clicked += OnClickPrevSlide;

        btnFinishTest = root.Q<Button>("btnFinishTest");
        if(btnFinishTest == null)
        {
            btnIsPhishing = root.Q<Button>("btnIsPhishing");
            btnNotPhishing = root.Q<Button>("btnNotPhishing");
            //btnIsPhishing.clicked += UpdateSlidesArray;
            //btnNotPhishing.clicked += UpdateSlidesArray;
        }
        else 
        {
            //btnFinishTest.clicked += UpdateSlidesArray;
        } 
    }

    private void Update()
    {
        if(!isUpdateEnabled)
        {
            return;
        }

        if(currentSlideIndex != 0)
        {
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 1f);
            btnPrevSlide.SetEnabled(true);
        }
        else if (currentSlideIndex == 0)
        {
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnPrevSlide.SetEnabled(false);
        }

        if (currentSlideIndex == slidesArray.Length - 1) 
        {
            btnNextSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnNextSlide.SetEnabled(false);
        }
        else 
        {
            btnNextSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 1f);
            btnNextSlide.SetEnabled(true);
        }
    }

    private void ShowSlide(int index)
    {
        slidesArray[index].style.display = DisplayStyle.Flex;
    }

    private void OnClickNextSlide()
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        currentSlideIndex ++;
        ShowSlide(currentSlideIndex);
    }

    private void OnClickPrevSlide()
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
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

    /* private void UpdateSlidesArray()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();

        if (slidesArray.Length < 2) 
        {
            btnNextSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnNextSlide.SetEnabled(false);
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnPrevSlide.SetEnabled(false);
            isUpdateEnabled = false;
        }
    } */

 
}
