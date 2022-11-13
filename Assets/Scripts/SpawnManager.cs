using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefabs;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private int _randomEnemy;

    private bool _stopSpawn = true;
    [SerializeField]
    private float _spawnTime = 2f;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject[] _rarePowerups;
    [SerializeField]
    private GameObject _PlasmaShot;

    [SerializeField]
    private int _startingWave = 1;
    private int _currentWave;
    private int _enemySpawnAmount;
    [SerializeField]
    private int _currentEnemies;
    private bool _enemySpawnComplete;

    UIManager _UIManager;

    // Start is called before the first frame update
    void Start()
    {
        _currentWave = _startingWave;

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if( _UIManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
    }

    private void Update()
    {
        if (_stopSpawn == false && _currentEnemies == 0 && _enemySpawnComplete == true)
        {
            StartNewWave();

        }
    }



    public void StartSpawn()
    {
        _stopSpawn = false;
    
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnRarePowerupRoutine());
        
    }

    void StartNewWave()
    {
        _currentWave++;
        _UIManager.UpdateWave(_currentWave);
        StartCoroutine(SpawnEnemyRoutine());

    }

   
    public void DestroyedEnemy()
    {
        _currentEnemies--;
        _UIManager.UpdateEnemies(_currentEnemies);
    }

    

    IEnumerator SpawnEnemyRoutine()
    {
        _enemySpawnComplete = false;
        int enemyAmount = Random.Range(3, 5);
        _enemySpawnAmount = enemyAmount * _currentWave;
        

        yield return new WaitForSeconds(3f);

        while (_stopSpawn == false && _enemySpawnAmount > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
            _randomEnemy = Random.Range(0, 4);
            GameObject newEnemy = Instantiate(_enemyPrefabs[_randomEnemy], spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemySpawnAmount--;
            _currentEnemies++;
            _UIManager.UpdateEnemies(_currentEnemies);
            yield return new WaitForSeconds(_spawnTime);

        }

        _enemySpawnComplete = true;
        yield return null;
    }

   

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(_stopSpawn == false)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            int RandomPowerup = Random.Range(0, 3);
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
            int RandomRarePowerup = Random.Range(0, 3);
            Instantiate(_rarePowerups[RandomRarePowerup], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(25f, 40f));


        }


    }
    public void OnPlayerDeath()
    {

        _stopSpawn = true;


    }




}
