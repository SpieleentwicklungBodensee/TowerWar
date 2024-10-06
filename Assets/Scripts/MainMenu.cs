using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject joinScreen;
    public TextMeshProUGUI joinText;
    
    private PlayerInput _player1Input;
    private PlayerInput _player2Input;

    private void Start()
    {
        joinScreen.SetActive(false);
        PlayerInputManager.instance.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Exit();
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        joinScreen.SetActive(true);
        SetJoinTextFor(1);
        PlayerInputManager.instance.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenJoinActionIsTriggered;
        PlayerInputManager.instance.EnableJoining();
        PlayerInputManager.instance.onPlayerJoined += PlayerJoined;
    }

    private void PlayerJoined(PlayerInput playerInput)
    {
        DontDestroyOnLoad(playerInput.gameObject);
        
        if (!_player1Input)
        {
            _player1Input = playerInput;
            playerInput.GetComponent<InputControl>().isPlayerOne = true;
            SetJoinTextFor(2);
        }
        else
        {
            _player2Input = playerInput;
            playerInput.GetComponent<InputControl>().isPlayerOne = false;
            PlayerInputManager.instance.DisableJoining();
            LoadGame();
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    private void SetJoinTextFor(int playerNumber)
    {
        joinText.text = "Player " + playerNumber + " - Press START or Space";
    }
}