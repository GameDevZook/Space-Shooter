using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   

   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(new Vector3(0, -4, 0) * Time.deltaTime);

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
            
            Destroy(this.gameObject);

        }

        
        //if other is laser
        //destroy laser
        //destroy us

     if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }

    }

}
