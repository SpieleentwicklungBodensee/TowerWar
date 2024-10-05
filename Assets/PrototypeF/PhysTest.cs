using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysTest : MonoBehaviour
{
    private bool hasBeenDragged;

    private void OnMouseDown()
    {
        hasBeenDragged = false;
    }

    private void OnMouseUp()
    {
        if (!hasBeenDragged)
            Destroy(gameObject);
        
        hasBeenDragged = false;
    }
}
