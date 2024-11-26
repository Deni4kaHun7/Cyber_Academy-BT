using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private string slideName;
    [SerializeField] private string btnsContainerName;

    private int currentSlideIndex = 0;
    private Button btnNextSlide;
    private Button btnPrevSlide;
    private VisualElement[] slidesArray;
    private VisualElement nextSlide;
    private VisualElement prevSlide;
    private VisualElement btnsContainer;

    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;

        slidesArray = root.Query<VisualElement>(slideName).ToList().ToArray();

        btnsContainer = root.Query<VisualElement>(btnsContainerName);
        btnNextSlide = btnsContainer.Query<Button>("btnNextSlide");
        btnNextSlide.clicked += OnClickNextSlide;
        
        btnPrevSlide = btnsContainer.Query<Button>("btnPrevSlide");
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
