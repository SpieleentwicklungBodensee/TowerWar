using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class InputControl : MonoBehaviour
{
    float delta = 0;
    bool fire = false;
    double fireStartTime = 0;
    public Image directionImage;
    public Slider powerSlider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        directionImage.transform.Rotate(0, 0, -90 * delta * Time.deltaTime);

        if(fire)
            powerSlider.value = Math.Min((float)(Time.timeAsDouble - fireStartTime), 1.0f);
        else
            powerSlider.value = 0.0f;
    }

    public void TargetDirection(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<float>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() != 0)
        {
            if(!fire)
            {
                fire = true;
                fireStartTime = Time.timeAsDouble;
            }
        }
        else fire = false;
    }
}
