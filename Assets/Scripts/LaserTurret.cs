using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float _speed = 1f;
    private bool _canFire;
    private bool _isFiring;
    [SerializeField]
    private GameObject _laserPrefab;


    void Start()
    {
        
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
         CalculateMovement();
        
        if (transform.position.y < _player.transform.position.y)
        {
            _canFire = true;
            if (_isFiring == false)
            {
                StartCoroutine(ShootLaserRoutine());
            }
        }
        else
        {
            _canFire = false;
        }


    }

    IEnumerator ShootLaserRoutine()
    {
        _isFiring = true;
        Vector3 localOffset = new Vector3(0, 1.4f, 0);

        while(_canFire == true)
        {
            
            
            Instantiate(_laserPrefab, transform.position + localOffset, transform.rotation);

            yield return new WaitForSeconds(.5f);


        }

        yield return null;
        _isFiring = false;
        
    }

    void CalculateMovement()
    {
        
        

        float speed = _speed * Time.deltaTime;
        Vector3 targetDirection = _player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = rotation;
        

    }


}
