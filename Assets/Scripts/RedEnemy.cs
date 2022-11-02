using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _lives;
    
    SpawnManager _spawnManager;
    Player _player;

    [SerializeField]
    private GameObject _plasmaShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    
    



    // Start is called before the first frame update
    void Start()
    {
        _lives = 3;
        StartCoroutine(ShootLaserRoutine());

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if( _spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }

        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {

            Debug.LogError("Player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {

            Damage();
            
            Destroy(other.gameObject);
            
        }

        if(other.tag == "Player")
        {
            _player.Damage();
            EnemyDeathSequence();

        }
    }

    void Damage()
    {
        _lives--;
        if (_lives == 2)
        {
            _leftEngine.SetActive(true);

        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
            _leftEngine.SetActive(true);

        }
        if (_lives <= 0)
        {
            EnemyDeathSequence();
            
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.7f)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 spawnPos = new Vector3(randomX, 8, 0);
            transform.position = spawnPos;

        }
    }


    void EnemyDeathSequence()
    {

        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _spawnManager.DestroyedEnemy();
        _player.UpdateScore(50);
        Destroy(this.gameObject);


    }

    IEnumerator ShootLaserRoutine()
    {
        Vector3 localOffset = new Vector3(0, -1.4f, 0);
        float fireRate = Random.Range(5f, 7f);

        yield return new WaitForSeconds(3f);

        while (this.gameObject.activeSelf)
        {
            GameObject enemyPlasma = Instantiate(_plasmaShotPrefab, transform.position + localOffset, Quaternion.Euler(0, 0, 180));
            
            PlasmaShot[] plasma = enemyPlasma.GetComponentsInChildren<PlasmaShot>();

            for (int i = 0; i < plasma.Length; i++)
            {
                plasma[i].AssignEnemyLaser();

            }

            yield return new WaitForSeconds(fireRate);
        }


    }

}
