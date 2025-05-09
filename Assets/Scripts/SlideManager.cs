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
        // Find the UIDocument in the scene
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        // Get the root element of the UI hierarchy
        var root = uiDocument.rootVisualElement;
        // Initialize Visual Element which holds all the slides
        slideParentContainer = root.Q<VisualElement>(slideContainerName);
        // Store all the slides to an array
        slidesArray = slideParentContainer.Query<Label>().ToList().ToArray();
        // Disable Update method if there are less than two slides
        if(slidesArray.Length < 2)
        {
            isUpdateEnabled = false;
        }
        //Initialize a Visual element with navigation buttons
        btnsContainer = root.Q<VisualElement>(btnsContainerName);
        // Initialize a Button element with name "btnNextSlide"
        btnNextSlide = btnsContainer.Query<Button>("btnNextSlide");
        // Assign the OnClickNextSlide method to the "clicked" to the button
        btnNextSlide.clicked += OnClickNextSlide;
        // Disable button by default
        btnNextSlide.SetEnabled(false);
        
        // Initialize a Button element with name "btnPrevSlide"
        btnPrevSlide = btnsContainer.Q<Button>("btnPrevSlide");
        //
        btnPrevSlide.clicked += OnClickPrevSlide;
        btnPrevSlide.SetEnabled(false);

        // Initialize a Button element with name "btnHideIntro"
        btnHideIntro = root.Q<Button>("btnHideIntro");
        btnHideIntro.SetEnabled(false);
    }

    private void Update()
    {
        // Check if there are more than two slides
        if(!isUpdateEnabled)
        {
            return;
        } 
        // Enable btnPrevSlide if the current slide is not the first one
        if(currentSlideIndex != 0)
        {
            btnPrevSlide.style.opacity = 1f;
            btnPrevSlide.SetEnabled(true);
        }
        // Disable btnPrevSlide if the current slide is the first one
        else if (currentSlideIndex == 0)
        {
            btnPrevSlide.style.opacity = .07f;
            btnPrevSlide.SetEnabled(false);
        }
        // Disable btnPrevSlide and enable btnHideIntro if the current slide is the last one
        if (currentSlideIndex == slidesArray.Length - 1) 
        {
            btnNextSlide.style.opacity = .07f;
            btnNextSlide.SetEnabled(false);

            btnHideIntro.style.opacity = 1f;
            btnHideIntro.SetEnabled(true);
        }
        // Enable the btnNextSlide
        else 
        {
            btnNextSlide.style.opacity = 1f;
            btnNextSlide.SetEnabled(true);
        }
    }

    // Method to switch to the next slide
    private void OnClickNextSlide()
    {
        // Hide the current slide
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        // Increase the currentSlideIndex by one
        currentSlideIndex ++;
        // Reveal the next slide in the array
        slidesArray[currentSlideIndex].style.display = DisplayStyle.Flex;
    }

    // Method to switch to the previous slide
    private void OnClickPrevSlide()
    {   
        // Hide the current slide
        slidesArray[currentSlideIndex].style.display = DisplayStyle.None;
        // Decrease the currentSlideIndex by one
        currentSlideIndex --;
        // Reveal the previous slide in the array
        slidesArray[currentSlideIndex].style.display = DisplayStyle.Flex;
    }

    // Creates a new SlideManager instance 
    public static void CreateSlideManager(string slideName, string btnContainer)
    {
        // Create a new GameObject named "SlidesManager" to manage slides
        GameObject slidesIntroManager = new GameObject("SlidesManager");
        // Add the SlideManager component to the newly created GameObject
        SlideManager slideManagerScript = slidesIntroManager.AddComponent<SlideManager>();
        // Assign the provided slide container name to the SlideManager script
        slideManagerScript.slideContainerName = slideName;
        // Assign the provided button container name to the SlideManager script
        slideManagerScript.btnsContainerName = btnContainer;
    }
}
