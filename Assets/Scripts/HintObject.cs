using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObject : MonoBehaviour
{
    // This method is called when the user clicks on the GameObject this script is attached to.
    private void OnMouseDown()
    {
       HintObjectManager hintManager = FindObjectOfType<HintObjectManager>();
       hintManager.OnClickHintObject(gameObject);
    }
}
