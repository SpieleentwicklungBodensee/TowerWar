using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public  TMP_Text winText;
    public  TMP_Text restartText;
    private Player   _player1;
    private Player   _player2;
    public bool     gameFinished;
    public bool     blockSelected;

    private Player _currentPlayer;

    void Start()
    {
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();

        _player1.OnDeath += () => GameOver("Player 2");
        _player2.OnDeath += () => GameOver("Player 1");

        winText.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);

        _currentPlayer = _player1;
        _currentPlayer.Activate(true);
        gameFinished = false;
        blockSelected = false;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    public int GetCurrentPlayer()
    {
        return _currentPlayer == _player1 ? 0 : 1;
    }

    public Player GetCurrentPlayerAsRef()
    {
        return _currentPlayer;
    }

    public void Fire(Vector2 v)
    {
        _currentPlayer.Shoot(v);
        SwitchPlayer();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BlockSelection(Vector2 direction)
    {
        if (gameFinished)
            return;

        _currentPlayer.ChangeBlockSelection(direction);
    }

    void SwitchPlayer()
    {
        _currentPlayer.Activate(false);
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        _currentPlayer.Activate(true);
        blockSelected = false;
    }

    void GameOver(string winner)
    {
        winText.text = winner + " wins!";
        winText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        gameFinished = true;
        _currentPlayer.Activate(false);
    }
}