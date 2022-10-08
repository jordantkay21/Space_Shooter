using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int _currentWave;

    [Header("Enemies to Spawn")]
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tankPrefab;
    [SerializeField]
    private GameObject _brutePrefab;
    [SerializeField]
    private GameObject _sniperPrefab;

    [Header("Collectables to Spawn")]
    [SerializeField]
    private GameObject[] _frequentPowerups;
    [SerializeField]
    private GameObject[] _regularPowerups;
    [SerializeField]
    private GameObject[] _rarePowerups;
    [SerializeField]
    private GameObject[] _powerdowns;

    private GameManager _gameManager;
    private GameObject _mothership;
    private Mothership _mothershipScript;

    [SerializeField]
    private bool _stopSpawning = false;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _mothership = GameObject.Find("Mothership");
        if(_mothership != null)
        {
            _mothershipScript = GameObject.Find("Mothership").GetComponent<Mothership>();
        }


        if (_gameManager == null)
        {
            Debug.LogError("Game_Manager is NULL.");
        }
    }
    public void StartSpawning()
    {
        _currentWave = _gameManager.CheckCurrentSceneIndex();

        switch (_currentWave)
        {
            case 0: //main menu
                return;
            case 1:
                Wave1Spawn();
                break;
            case 2:
                Wave2Spawn();
                break;
            case 3:
                Wave3Spawn();
                break;
            case 4:
                Wave4Spawn();
                break;
            case 5:
                Wave5Spawn();
                break;

        }
    }


    private void Wave1Spawn()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnFrequentPowerUpRoutine());        
        StartCoroutine(SpawnRegularPowerUpRoutine());
    }

    private void Wave2Spawn()
    {
        Wave1Spawn();        
        StartCoroutine(SpawnTankRoutine());        

        StartCoroutine(SpawnRarePowerUpRoutine());
    }

    private void Wave3Spawn()
    {
        Wave2Spawn();
        StartCoroutine(SpawnBruteRoutine());
        StartCoroutine(SpawnPowerDownRoutine());
    }

    private void Wave4Spawn()
    {
        Wave3Spawn();
        StartCoroutine(SpawnSniperRoutine());
    }

    private void Wave5Spawn()
    {
        StartCoroutine(SpawnFrequentPowerUpRoutine());
        StartCoroutine(SpawnRegularPowerUpRoutine());
        StartCoroutine(SpawnRarePowerUpRoutine());
        StartCoroutine(SpawnPowerDownRoutine());
        _mothershipScript.FirstMove();
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnTankRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 12.0f));
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(-10, Random.Range(6, 3), 0);
            GameObject newEnemy = Instantiate(_tankPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }

    IEnumerator SpawnSniperRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 12.0f));
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            GameObject newEnemy = Instantiate(_sniperPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }
    IEnumerator SpawnBruteRoutine()
    {
        yield return new WaitForSeconds(Random.Range(3.0f, 12.0f));
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            GameObject newEnemy = Instantiate(_brutePrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        }
    }

    IEnumerator SpawnFrequentPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(_frequentPowerups[Random.Range(0, 0)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 10));
        }
    }

    IEnumerator SpawnRegularPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(_regularPowerups[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 14));
        }
    }

    IEnumerator SpawnRarePowerUpRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(_rarePowerups[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(14, 18));
        }
    }

    IEnumerator SpawnPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(_powerdowns[Random.Range(0, 0)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 18));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
