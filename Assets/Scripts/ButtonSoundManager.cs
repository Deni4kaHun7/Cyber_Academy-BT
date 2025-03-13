using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonSoundManager : MonoBehaviour
{
    public static ButtonSoundManager Instance { get; private set;}
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void RegisterButtons(UIDocument uiDocument) {
        var root = uiDocument.rootVisualElement;
        var buttons = root.Query<Button>().ToList();

        foreach (var btn in buttons)
        {
            btn.clicked += () => audioSource.Play();
        }
    }
}
