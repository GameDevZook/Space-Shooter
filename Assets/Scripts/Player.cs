using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private bool _isShieldPowerupActive = false;
    private bool _isTripleShotActive = false;
    private bool _isSpeedPowerupActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private AudioClip _fireLaserAudio;
    [SerializeField]
    private AudioSource _audioSource;


    [SerializeField]
    private int _score;

    private UIManager _uiManager;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
      

        if(_uiManager == null)
        {
            Debug.LogError("_uiManager returned NULL");
        }

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is Null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL");
        }
        else
        {
            _audioSource.clip = _fireLaserAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();

        }

        
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.96f, 0), Quaternion.identity);
            
        }

        _audioSource.Play();
     
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_isSpeedPowerupActive == true)
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * _speedMultiplier * Time.deltaTime);
        }

        else
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y <= -3.8f)
        {

            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }


        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }


    }

    public void Damage()
    {
        if (_isShieldPowerupActive == false)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
            
        }

        else
        {
            _isShieldPowerupActive = false;
            _playerShield.SetActive(false);
            return;
        }

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            

        }
    }



    public void ShieldPowerupActive()
    {
        _isShieldPowerupActive = true;
        _playerShield.SetActive(true);

    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotDurationRoutine());

    }

    public void SpeedPowerupActive()
    {

        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerupDurationRoutine());

    }
    IEnumerator TripleShotDurationRoutine()
    {

        while (_isTripleShotActive == true)
        {
            _audioSource.pitch = 0.8f;
            yield return new WaitForSeconds(10f);
            _audioSource.pitch = 1;
            _isTripleShotActive = false;

        }


    }

    IEnumerator SpeedPowerupDurationRoutine()
    {
        while (_isSpeedPowerupActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isSpeedPowerupActive = false;


        }

    }

   

    public void UpdateScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}

