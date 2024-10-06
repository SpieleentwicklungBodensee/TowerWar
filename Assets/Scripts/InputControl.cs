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

    public bool isPlayerOne = true;

    public GameObject directionImage;
    public Image powerSlider;

    public GameController gameController;
    Vector2 currentBlockSelection;

    public Player attachedPlayer;

    // Update is called once per frame
    void Update()
    {
        if (!attachedPlayer)
            return;
        
        rotation -= 90 * delta * Time.deltaTime;

        if(fire)
            power = Math.Min((float)(Time.timeAsDouble - fireStartTime), 1.0f);
        else
            power = 0.0f;

        UpdateUi();
    }

    private void UpdateUi()
    {
        if (gameController.gameFinished)
        {
            directionImage.gameObject.SetActive(false);
        }
        else
        {
            if ((gameController.GetCurrentPlayer() == 0 && isPlayerOne) || (gameController.GetCurrentPlayer() == 1 && !isPlayerOne))
            {
                directionImage.gameObject.SetActive(gameController.blockSelected);
            }
            else
            {
                directionImage.gameObject.SetActive(false);
            }
        }

        directionImage.transform.eulerAngles = new Vector3(0, 0, rotation);
        powerSlider.fillAmount = power;
    }

    public void TargetDirection(InputAction.CallbackContext context)
    {
        if (!attachedPlayer || gameController.GetCurrentPlayer() != (isPlayerOne ? 0 : 1))
            return;
        
        if(!gameController.blockSelected)
            return;

        delta = context.ReadValue<float>();
    }

    void Fire(bool pressed)
    {
        if(pressed)
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

            //rotation = (gameController.GetCurrentPlayer() == 0)? 0 : 180;
        }
    }

    public void PrimaryAction(InputAction.CallbackContext context)
    {
        if (!attachedPlayer || gameController.GetCurrentPlayer() != (isPlayerOne ? 0 : 1))
            return;
        
        if (gameController.gameFinished)
        {
            gameController.ReloadGame();
        }

        bool pressed = context.ReadValue<float>() != 0;
        if(gameController.blockSelected)
            Fire(pressed);
        else if(!pressed)
            gameController.blockSelected = true;
    }

    public void BlockSelection(InputAction.CallbackContext context)
    {
        if (!attachedPlayer || gameController.GetCurrentPlayer() != (isPlayerOne ? 0 : 1))
            return;
        
        if(gameController.blockSelected)
            return;

        Vector2 direction = context.ReadValue<Vector2>();
        if(direction.magnitude < 0.7 && currentBlockSelection.magnitude > 0.8)
            gameController.BlockSelection(currentBlockSelection.normalized);

        currentBlockSelection = direction;
    }
}
