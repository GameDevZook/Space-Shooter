using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private SpawnManager _spawnManager;
    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if( _spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");

        }

        _player = GameObject.Find("Player").GetComponent<Player>();
        if ( _player == null)
        {
            Debug.LogError("Player is NULL");

        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement() { 

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.7f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    void DeathSequence()
    {

        _player.UpdateScore(20);
        _spawnManager.DestroyedEnemy();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            DeathSequence();

        }

        if (collision.tag == "Player")
        {
            _player.Damage();
            DeathSequence();

        }
    }


}
