using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [SerializeField]
    private float _speed = 5f;

    private Player _player;
    private SpawnManager _spawnManager;

    [SerializeField]
    private GameObject _enemyLaser;
    [SerializeField]
    private GameObject _enemyShield;
    

    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;
    

    Animator enemyExplode_anim;

    [SerializeField]
    private int _moveID;

    

    

    // Start is called before the first frame update
    void Start()
    {
        SpawnShield();

        StartCoroutine(ShootLaserRoutine());
        StartCoroutine(SetMovementRoutine());


        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Enemy Explosion audio source is NULL");
        }

      _player =  GameObject.Find("Player").GetComponent<Player>();
        if( _player == null)
        {
            Debug.LogError("_player returned NULL");
        }

       _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        enemyExplode_anim = GetComponent<Animator>();
        if( enemyExplode_anim == null)
        {
            Debug.LogError("Animator returned NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

       
         CalculateEnemyMovement();
         
    }


    void CalculateEnemyMovement()
    {
        Vector3 moveRight = new Vector3(2, -_speed, 0);
        Vector3 moveLeft = new Vector3(-2, -_speed, 0);

        switch (_moveID)
        {
            case 0:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;

            case 1:
                transform.Translate(moveLeft * Time.deltaTime);
                break;

            case 2:
                transform.Translate(moveRight * Time.deltaTime);
                break;

            default:
                Debug.Log("Default Value");
                break;

        }
      
        

        if (transform.position.y <= -5.7f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }

    }

   
    private void SpawnShield()
    {
        int ShieldRate = Random.Range(0, 7);

        if (ShieldRate == 0)
        {
            _enemyShield.SetActive(true);


        }

        else return;


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       

     if(other.tag == "Player")
        {
           

            _spawnManager.DestroyedEnemy();

            _player.Damage();

            EnemyDeathSequence();

        }

     if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_enemyShield.activeSelf == true)
            {
                _enemyShield.SetActive(false);

            }

            else
            {
                _spawnManager.DestroyedEnemy();


                _player.UpdateScore(10);


                EnemyDeathSequence();

            }
            

        }
      
     if(other.tag == "Enemy")
        {

            _moveID = 0;

        }
     




     void EnemyDeathSequence()
        {
            _speed = 0;
            _audioSource.clip = _explosionAudio;

            enemyExplode_anim.SetTrigger("OnEnemyDeath");
            GetComponent<BoxCollider2D>().enabled = false;
            _audioSource.Play();
            StopCoroutine(ShootLaserRoutine());

            Destroy(this.gameObject, 2.2f);

        }

    }

    IEnumerator ShootLaserRoutine()
    {
       

        while (this.gameObject.activeSelf)
        {

            Vector3 localOffset = new Vector3(0, -1.4f, 0);
        Instantiate(_enemyLaser, transform.position + localOffset, Quaternion.identity); 
         yield return new WaitForSeconds(5);
        }

    }

   

    IEnumerator SetMovementRoutine()
    {
        int randomMovement = Random.Range(0, 3);
        float randomMoveTime = Random.Range(2f, 4f);

        yield return new WaitForSeconds(2f);

        while (this.gameObject.activeSelf == true)
        {
            _moveID = randomMovement;
            yield return new WaitForSeconds(randomMoveTime);


        }



    }

}
