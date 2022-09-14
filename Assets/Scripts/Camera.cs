using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField]
    private AnimationCurve curve;

    private float _duration = 1f;
    
     

    

    IEnumerator StartShakeRoutine()
    {
        Vector3 StartPos = transform.position;
        float elapsedTime = 0f;
        

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / _duration);

            transform.position = StartPos + Random.insideUnitSphere * strength;

            yield return null;


        }

        transform.position = StartPos;

    }

    public void StartShake()
    {

        StartCoroutine(StartShakeRoutine());

    }
   















    
   

}
