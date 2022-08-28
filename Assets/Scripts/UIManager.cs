using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _ammoText;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _reloadText;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _livesImage;
    private int _currentLives;
    GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager reutrned NULL");

        }

        _reloadText.gameObject.SetActive(false);
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score " + 0;
       
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
        
    }

    public void UpdateAmmo(int currentAmmo)
    {

        _ammoText.text = "Ammo: " + currentAmmo;
    }

    public void UpdateLives(int _currentLives)
    {
    _livesImage.sprite = _liveSprites[_currentLives];

        if (_currentLives == 0) 
        {
            GameOverSequence();
            
        }
    }


    private void GameOverSequence()
    {
        _reloadText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        _gameManager.GameOver();
    }

    public IEnumerator GameOverFlickerRoutine()
    {
       while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
       
    }

 
}
