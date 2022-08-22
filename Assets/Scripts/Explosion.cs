using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
  
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();

    }
    // Update is called once per frame
    void Update()
    {
       
        Destroy(this.gameObject, 2.6f);
    }
}
