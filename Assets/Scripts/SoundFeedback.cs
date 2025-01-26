using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        audioSource.Play();
    }
}
