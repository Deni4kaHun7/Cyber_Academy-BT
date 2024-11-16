using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private Button[] btnNextSceneArray;
    void Start()
    {
        var uiDocument = GameObject.FindObjectOfType<UIDocument>();
        var root = uiDocument.rootVisualElement;
        btnNextSceneArray = root.Query<Button>("btnNextScene").ToList().ToArray(); 

        foreach(var btn in btnNextSceneArray)
        {
            btn.clicked += OnClickNextScene;
        }
    }

    private void OnClickNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
