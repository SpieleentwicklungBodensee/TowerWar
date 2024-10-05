using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    public  TMP_Text winText;
    private Player   _player1;
    private Player   _player2;
    
    void Start()
    {
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();

        _player1.OnDeath += () => GameOver("Player 2");
        _player2.OnDeath += () => GameOver("Player 1");
        
        winText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    void GameOver(string winner)
    {
        winText.text = winner + " wins!";
        winText.gameObject.SetActive(true);
    }
}