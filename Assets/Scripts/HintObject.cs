using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HintObject : MonoBehaviour
{
    // This method is called when the user clicks on the GameObject this script is attached to.
    private void OnMouseDown()
    {
        // Call the static method OnClickHintObject from the HintObjectManager class.
        // Pass the current GameObject (this one) as a parameter, so it knows which object was clicked.
        HintObjectManager.OnClickHintObject(gameObject);
    }
}
