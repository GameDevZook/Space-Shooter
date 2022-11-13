using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;
    
    private bool _isPlayerDetected = false;

    
    
    private GameObject _player;
    private UIManager _UIManager;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
    


        

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");

        }


    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if( _isPlayerDetected == true)
        {

            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            

        }

        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

        }
       

        if (transform.position.y < -5.7f)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 spawnPos = new Vector3(randomX, 8, 0);

        }

    }

    public void PlayerDetected()
    {

        _isPlayerDetected = true;


    }

    void DeathSequence()
    {
        

        _player.GetComponent<Player>().UpdateScore(20);
        _spawnManager.DestroyedEnemy();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            _player.GetComponent<Player>().Damage();
            DeathSequence();

        }

        if(collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            DeathSequence();

        }
    }

}
