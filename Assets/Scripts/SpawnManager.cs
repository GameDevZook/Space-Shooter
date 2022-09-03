using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawn = false;
    [SerializeField]
    private float _spawnTime = 2f;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject _PlasmaShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnRarePowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawn == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnTime);

        }

    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(_stopSpawn == false)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            int RandomPowerup = Random.Range(0, 5);
            Instantiate(powerups[RandomPowerup], SpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 14f));

        }


    }

    IEnumerator SpawnRarePowerupRoutine()
    {
        yield return new WaitForSeconds(30);

        while(_stopSpawn == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            Instantiate(_PlasmaShot, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(25f, 40f));


        }


    }
    public void OnPlayerDeath()
    {

        _stopSpawn = true;


    }




}
