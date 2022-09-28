using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _frequentPowerups;

    [SerializeField]
    private GameObject[] _regularPowerups;

    [SerializeField]
    private GameObject[] _rarePowerups;

    [SerializeField]
    private GameObject[] _powerdowns;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnFrequentPowerUpRoutine());
        StartCoroutine(SpawnRegularPowerUpRoutine());
        StartCoroutine(SpawnRarePowerUpRoutine());
        StartCoroutine(SpawnPowerDownRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
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
            Instantiate(_rarePowerups[Random.Range(0, 2)], posToSpawn, Quaternion.identity);
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
