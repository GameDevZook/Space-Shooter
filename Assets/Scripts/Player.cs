using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _thrusterSpeed = 1.5f;
    [SerializeField]
    private float _thrusterAmount;
    private bool _isThrusterActive;
    

    [SerializeField]
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _plasmaShotPrefab;
    [SerializeField]
    private float _fireRate = .5f;
    private float _canFire = -1f;
    private int _ammoCount;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _shieldLives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private GameObject _explosionPrefab;
   

    private Renderer _playerShieldColor;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private bool _isShieldPowerupActive = false;
    private bool _isTripleShotActive = false;
    private bool _isSpeedPowerupActive = false;
    [SerializeField]
    private bool _isPlasmaShotActive = false;

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
        _ammoCount = 100;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _playerShieldColor = _playerShield.GetComponent<Renderer>();
        _thrusterAmount = 1;
       

        if( _playerShieldColor == null)
        {
            Debug.LogError("PlayerShieldColor is NULL");
        }
      

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

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _ammoCount > 0)
        {
            FireLaser();
           _ammoCount--;
            _uiManager.UpdateAmmo(_ammoCount);
       
        }

       
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        else if (_isPlasmaShotActive == true)
        {

            Instantiate(_plasmaShotPrefab, transform.position + new Vector3(0, 0.96f, 0), Quaternion.identity);

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

        Thrusters();
       
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
            ShieldBehavior();
            
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
        _playerShield.SetActive(true);
        _isShieldPowerupActive = true;
        

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

    public void AmmoPickupActive()
    {

        _ammoCount += 100;
        _uiManager.UpdateAmmo(_ammoCount);

    }

    public void PlasmaShotActive()
    {
        _isPlasmaShotActive = true;
        StartCoroutine(PlasmaShotDurationRoutine());

    }

    public void AddLife()
    {
        if(_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);

            if(_lives == 3)
            {
                _leftEngine.SetActive(false);
                _rightEngine.SetActive(false);
            }

            else if (_lives == 2)
            {
                _rightEngine.SetActive(false);

            }
        }
        

        else
        {
            return;
        }

    }
    
        
        IEnumerator PlasmaShotDurationRoutine()
    {
        while(_isPlasmaShotActive == true)
        {
            yield return new WaitForSeconds(5f);
            _isPlasmaShotActive=false;

        }


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

    void Thrusters()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && _thrusterAmount > 0)
        {
            
            _isThrusterActive = true;
            StartCoroutine(UseThrusterRoutine());
            StopCoroutine(ThrusterRechargeRoutine());
            


        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isThrusterActive = false;
            StopCoroutine(UseThrusterRoutine());
            StartCoroutine(ThrusterRechargeRoutine());
            

        }

       
        
    }

    IEnumerator UseThrusterRoutine()
    {
        _speed *= _thrusterSpeed;

        while (_isThrusterActive == true && _thrusterAmount > 0)
        {
            
            _thrusterAmount -= 0.01f;
            _uiManager.SetBoost(_thrusterAmount);
            yield return new WaitForSeconds(0.01f);
            
        }
       
       _speed /= _thrusterSpeed;
        

    }
    IEnumerator ThrusterRechargeRoutine()
    {
        while (_thrusterAmount < 1 && _isThrusterActive == false)
        {
            _thrusterAmount += 0.01f;
            _uiManager.SetBoost(_thrusterAmount);
            yield return new WaitForSeconds(0.01f);

        }



    }

    void ShieldBehavior()
    {
        _shieldLives--;

        if(_shieldLives == 2)
        {

            _playerShieldColor.material.color = new Color32(222, 222, 222, 255);

        }

        if(_shieldLives == 1)
        {
            _playerShieldColor.material.color = new Color32(135, 135, 135, 255);

        }

        if(_shieldLives == 0)
        {
            _playerShieldColor.material.color = Color.white;
            _playerShield.SetActive(false);
            _isShieldPowerupActive = false;
        }

    }
}


