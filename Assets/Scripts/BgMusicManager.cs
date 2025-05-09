using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager : MonoBehaviour
{
    void Awake()
    {
        //Keep the music playing after transitioning to the next scene
        DontDestroyOnLoad(gameObject);
    }
}
