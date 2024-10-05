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
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab);
            _currentPlayer.Shoot(new Vector2(_currentPlayer == _player1 ? 400 : -400, 100), bullet);
            SwitchPlayer();
        }
    }

    void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
    }

    void GameOver(string winner)
    {
        winText.text = winner + " wins!";
        winText.gameObject.SetActive(true);
    }
}