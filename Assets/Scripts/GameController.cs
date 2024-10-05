using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public  TMP_Text   winText;
    public  GameObject bulletPrefab;
    private Player     _player1;
    private Player     _player2;
    private bool       gameFinished;

    private Player _currentPlayer;

    void Start()
    {
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();

        _player1.OnDeath += () => GameOver("Player 2");
        _player2.OnDeath += () => GameOver("Player 1");

        winText.gameObject.SetActive(false);

        _currentPlayer = _player1;
        _currentPlayer.Activate(true);
        gameFinished = false;
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

    public void Fire(Vector2 v)
    {
        if(gameFinished)
            return;

        _currentPlayer.Shoot(v);
        SwitchPlayer();
    }

    public void BlockSelection(Vector2 direction)
    {
        if(gameFinished)
            return;

        _currentPlayer.ChangeBlockSelection(direction);
    }

    void SwitchPlayer()
    {
        _currentPlayer.Activate(false);
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        _currentPlayer.Activate(true);
    }

    void GameOver(string winner)
    {
        winText.text = winner + " wins!";
        winText.gameObject.SetActive(true);
        gameFinished = true;
        _currentPlayer.Activate(false);
    }
}