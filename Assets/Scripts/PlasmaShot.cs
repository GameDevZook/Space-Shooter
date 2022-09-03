using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaShot : MonoBehaviour
{
  
    private float _laserSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += (transform.up * _laserSpeed * Time.deltaTime);

        if(transform.position.y > 8)
        {

            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);

            }
            Destroy(this.gameObject);

        }
        
    }
}
