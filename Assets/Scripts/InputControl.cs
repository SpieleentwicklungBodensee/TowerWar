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

    public Image directionImagePlayer0;
    public Slider powerSliderPlayer0;

    public Image directionImagePlayer1;
    public Slider powerSliderPlayer1;

    public GameController gameController;
    Vector2 currentBlockSelection;

    // Start is called before the first frame update
    void Start()
    {

    }

    Image GetDirectionImage()
    {
        return (gameController.GetCurrentPlayer() == 0)? directionImagePlayer0 : directionImagePlayer1;
    }

    Slider GetPowerSlider()
    {
        return (gameController.GetCurrentPlayer() == 0)? powerSliderPlayer0 : powerSliderPlayer1;
    }

    // Update is called once per frame
    void Update()
    {
        rotation -= 90 * delta * Time.deltaTime;

        if(fire)
            power = Math.Min((float)(Time.timeAsDouble - fireStartTime), 1.0f);
        else
            power = 0.0f;

        UpdateUi();
    }

    void UpdateUi()
    {
        directionImagePlayer0.gameObject.SetActive(!gameController.gameFinished);
        powerSliderPlayer0.gameObject.SetActive(!gameController.gameFinished);
        directionImagePlayer1.gameObject.SetActive(!gameController.gameFinished);
        powerSliderPlayer1.gameObject.SetActive(!gameController.gameFinished);

        GetDirectionImage().transform.eulerAngles = new Vector3(0, 0, rotation);
        GetPowerSlider().value = power;
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
        else if(fire)
        {
            Vector2 dir = new Vector2((float)Math.Cos(rotation * Math.PI/180),
                                      (float)Math.Sin(rotation * Math.PI/180));
            Vector2 v = dir * (power + 0.2f) * 10000.0f;

            fire = false;
            rotation = (gameController.GetCurrentPlayer() == 0)? 0 : 180;
            power = 0.0f;
            UpdateUi();

            if(gameController)
                gameController.Fire(v);

            rotation = (gameController.GetCurrentPlayer() == 0)? 0 : 180;
        }
    }

    public void BlockSelection(InputAction.CallbackContext context)
    {
        if(!gameController)
            return;

        Vector2 direction = context.ReadValue<Vector2>();
        if(direction.magnitude < 0.7 && currentBlockSelection.magnitude > 0.8)
            gameController.BlockSelection(currentBlockSelection.normalized);

        currentBlockSelection = direction;
    }
}
