using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isGameOver;

    public void GameOver()
    {
        _isGameOver = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current Game Scene

        }

        if (Input.GetKeyDown(KeyCode.Escape)){

            SceneManager.LoadScene(0);
        }
    }
}
