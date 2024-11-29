using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private string slideContainerName;
    [SerializeField] private string btnsContainerName;
    //[SerializeField] private string slideParentName;
    private int currentSlideIndex = 0;
    private Button btnNextSlide;
    private Button btnPrevSlide;
    private Button btnFinishTest;
    private Label[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;
    private VisualElement btnsContainer;
    private VisualElement slideParentContainer;

    private void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slideParentContainer = root.Q<VisualElement>(slideContainerName);
        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();

        btnsContainer = root.Q<VisualElement>(btnsContainerName);
        btnNextSlide = btnsContainer.Query<Button>("btnNextSlide");
        Debug.Log(btnsContainer);
        btnNextSlide.clicked += OnClickNextSlide;
        
        btnPrevSlide = btnsContainer.Q<Button>("btnPrevSlide");
        btnPrevSlide.clicked += OnClickPrevSlide;

        btnFinishTest = root.Q<Button>("btnFinishTest");
        //btnFinishTest.clicked += OnClickFinishTest;
    }

    private void Update()
    {
        if(currentSlideIndex == 0)
        {
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnPrevSlide.SetEnabled(false);
        }
        else 
        {
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 1f);
            btnPrevSlide.SetEnabled(true);
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

        if (slidesArray.Length == 1) 
        {
            btnNextSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnNextSlide.SetEnabled(false);
            btnPrevSlide.style.unityBackgroundImageTintColor = new Color(1f ,1f ,1f, 0.04f);
            btnPrevSlide.SetEnabled(false);
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
        Debug.Log("dsds");
       /*  var nextSlideIndex = currentSlideIndex + 1;
        if(nextSlideIndex == slidesArray.Length)
        {
            btnNextLevel.style.display = DisplayStyle.Flex;
            btnNextSlide.SetEnabled(false);
        }  */
    }

    private void OnClickPrevSlide()
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        currentSlideIndex --;
        ShowSlide(currentSlideIndex);
    }

    private void OnClickFinishTest()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();
        Debug.Log(slidesArray.Length);
    }
}
