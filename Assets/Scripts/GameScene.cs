using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
}