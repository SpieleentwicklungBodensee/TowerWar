using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public  TMP_Text   winText;
    public  GameObject bulletPrefab;
    private Player     _player1;
    private Player     _player2;

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
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    public void Fire(Vector2 v)
    {
        _currentPlayer.Shoot(v);
        SwitchPlayer();
    }

    public void BlockSelection(Vector2 direction)
    {
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
    }
}