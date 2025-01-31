using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlideManager : MonoBehaviour
{
    private int currentSlideIndex = 0;
    private Button nextSlideBtn;
    private Button prevSlideBtn;
    private VisualElement[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;

    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = root.Query<VisualElement>("Slide").ToList().ToArray();
        Debug.Log(slidesArray.Length);
        nextSlideBtn = root.Q<Button>("nextSlideBtn");

        nextSlideBtn.clicked += OnClickNextSlide;
    }

    private void ShowSlide(int index)
    {
        Debug.Log(index);
        slidesArray[index].style.display = DisplayStyle.Flex;
    }

    private void OnClickNextSlide()
    {
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        currentSlideIndex ++;
        ShowSlide(currentSlideIndex);
    }
}
