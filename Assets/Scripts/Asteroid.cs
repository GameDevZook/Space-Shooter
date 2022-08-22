using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3f;
    
    [SerializeField]
    GameObject _asteroidPrefab;
    [SerializeField]
    GameObject _explosionPrefab;
   SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
       _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on z axis
        transform.Rotate(new Vector3(0, 0, _rotationSpeed) * Time.deltaTime);
        //transform.Translate(new Vector3(0, -_fallSpeed, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.StartSpawn();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
