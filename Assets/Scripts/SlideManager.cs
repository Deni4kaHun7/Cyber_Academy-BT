using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private string slideName;
    [SerializeField] private Button[] btnsToHideArray;

    private int currentSlideIndex = 0;
    private Button[] btnNextSlideArray;
    private Button[] btnPrevSlideArray;
    private VisualElement[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;

    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = root.Query<VisualElement>(slideName).ToList().ToArray();
        btnNextSlideArray = root.Query<Button>("btnNextSlide").ToList().ToArray();
        foreach(var btnNextSlide in btnNextSlideArray)
        {
            btnNextSlide.clicked += OnClickNextSlide;
        }

        btnPrevSlideArray = root.Query<Button>("btnPrevSlide").ToList().ToArray();
        foreach(var btnPrevSLide in btnPrevSlideArray)
        {
            btnPrevSLide.clicked += OnClickPrevSlide;
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
}
