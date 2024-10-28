using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObject : MonoBehaviour
{
    private void OnMouseDown()
    {
        ScoreManager.AddScore();
    }
}
