using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private Player _player;
    [SerializeField]
    private GameObject _enemyLaser;

    [SerializeField]
    private AudioClip _explosionAudio;
    private AudioSource _audioSource;

    Animator enemyExplode_anim;
    
   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootLaserRoutine());

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

        enemyExplode_anim = GetComponent<Animator>();
        if( enemyExplode_anim == null)
        {
            Debug.LogError("Animator returned NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);

        if (transform.position.y <= -5.7f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }
        

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage the player
        //destroy us

     if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

           if(player != null)
            {
                player.Damage();

            }

            EnemyDeathSequence();

        }

        
        //if other is laser
        //destroy laser
        //destroy us

     if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.UpdateScore(10);
            }

            EnemyDeathSequence();

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
    

}
