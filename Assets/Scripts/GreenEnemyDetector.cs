using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyDetector : MonoBehaviour
{
    [SerializeField]
    private GreenEnemy _greenEnemy;

    void Start()
    {
        _greenEnemy = GameObject.Find("Green_Enemy").GetComponent<GreenEnemy>();
        if( _greenEnemy == null)
        {
            Debug.LogError("Green enemy is NULL");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            _greenEnemy.PlayerDetected();
            Destroy(this.gameObject);


        }
    }

}
