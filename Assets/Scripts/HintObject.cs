using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObject : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Initalize audio source variable
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // This method is called when the user clicks on the GameObject this script is attached to.
    private void OnMouseDown()
    {
        // Find the HintObjectManager in the scene.
        HintObjectManager hintManager = FindObjectOfType<HintObjectManager>();
        // Call the OnClickHintObject method and pass the reference to clicked object
        hintManager.OnClickHintObject(gameObject);
        // Play the associated audio clip.
        audioSource.Play();
    }
}
