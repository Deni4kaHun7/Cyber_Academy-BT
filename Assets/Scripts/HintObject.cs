using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObject : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // This method is called when the user clicks on the GameObject this script is attached to.
    private void OnMouseDown()
    {
       HintObjectManager hintManager = FindObjectOfType<HintObjectManager>();
       hintManager.OnClickHintObject(gameObject);
       audioSource.Play();
    }
}
