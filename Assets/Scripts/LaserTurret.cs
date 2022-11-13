using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    
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
        

        while(_canFire == true)
        {
            
            
            Instantiate(_laserPrefab, transform.position, transform.rotation);

            yield return new WaitForSeconds(.5f);


        }

        yield return null;
        _isFiring = false;
        
    }

    void CalculateMovement()
    {

        Vector3 diff = _player.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot_z - 90);

        //transform.right = _player.transform.position - transform.position;
        

    }


}
