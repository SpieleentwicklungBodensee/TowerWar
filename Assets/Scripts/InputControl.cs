using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputControl : MonoBehaviour
{
    float delta = 0;
    bool fire = false;
    double fireStartTime = 0;
    float rotation = 0;
    float power = 0;
    public Image directionImage;
    public Slider powerSlider;
    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation -= 90 * delta * Time.deltaTime;
        directionImage.transform.eulerAngles = new Vector3(0, 0, rotation);

        if(fire)
            power = Math.Min((float)(Time.timeAsDouble - fireStartTime), 1.0f);
        else
            power = 0.0f;

        powerSlider.value = power;
    }

    public void TargetDirection(InputAction.CallbackContext context)
    {
        Debug.Log("Hi!");
        delta = context.ReadValue<float>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Hi!");
        if(context.ReadValue<float>() != 0)
        {
            if(!fire)
            {
                fire = true;
                fireStartTime = Time.timeAsDouble;
            }
        }
        else if(fire)
        {
            if(gameController)
            {
                Vector2 dir = new Vector2((float)Math.Cos(rotation * Math.PI/180), (float)Math.Sin(rotation * Math.PI/180));
                gameController.Fire(dir * (power + 0.2f) * 10000.0f);
            }
            fire = false;
        }
    }

    public void BlockSelection(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        if(gameController)
            gameController.BlockSelection(direction);
    }
}
