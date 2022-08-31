using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private AudioClip _powerupAudio;
    


    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _PowerupID; //0 == TripleShot, 1 == Speed, 2 == Shields

    
    

    void Update()
    {
        CalculateMovement();
        
    }

    void CalculateMovement()
    {
        transform.Translate(new Vector3(0, -_speed, 0) * Time.deltaTime);

        if(transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

           AudioSource.PlayClipAtPoint(_powerupAudio, transform.position);

            Player player = collision.transform.GetComponent<Player>();
            if(player != null)
            {
                switch (_PowerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;

                    case 1:
                        player.SpeedPowerupActive();
                        break;

                    case 2:
                        player.ShieldPowerupActive();
                        break;

                    case 3:
                        player.AmmoPickupActive();
                        break;

                    default:
                        Debug.Log("Default Value");
                        break;

                }
            }

          
           

            Destroy(this.gameObject);

        }
    }
}