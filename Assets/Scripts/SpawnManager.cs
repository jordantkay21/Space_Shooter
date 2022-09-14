﻿using System.Collections;
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
    private GameObject[] _powerups;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
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

    
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 8, 0);
            Instantiate(_powerups[Random.Range(0, 3)], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
