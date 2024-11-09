using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlideManager : MonoBehaviour
{
    private int currentSlideIndex = 0;
    private Button btnNextSlide;
    private Button btnPrevSlide;
    private Button btnNextScene;
    private VisualElement[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;

    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = root.Query<VisualElement>("Slide").ToList().ToArray();
        btnNextSlide = root.Q<Button>("btnNextSlide");
        btnPrevSlide = root.Q<Button>("btnPrevSlide");
        btnNextScene = root.Q<Button>("btnNextScene");

        btnNextSlide.clicked += OnClickNextSlide;
        btnPrevSlide.clicked += OnClickPrevSlide;
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

        var nextSlideIndex = currentSlideIndex + 1;
        if(nextSlideIndex == slidesArray.Length)
        {
            btnNextScene.style.display = DisplayStyle.Flex;
            btnNextSlide.SetEnabled(false);
        } 
    }

    private void OnClickPrevSlide()
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        currentSlideIndex --;
        ShowSlide(currentSlideIndex);
    }
}
