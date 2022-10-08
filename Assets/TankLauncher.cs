using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankLauncher : MonoBehaviour
{
    [Header("Fire Tank Prefab")]
    [SerializeField]
    private bool _fireTank;
    [SerializeField]
    private GameObject _tankPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private void Start()
    {
        
    }

    public void SetFireTank()
    {
        _fireTank = true;
        StartCoroutine(SpawnTankRoutine());
    }

    public void FireTankFalse()
    {
        _fireTank = false;
    }
    IEnumerator SpawnTankRoutine()
    {
        yield return new WaitForEndOfFrame();
        while (_fireTank == true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
            Vector3 posToSpawn = transform.position;
            GameObject newEnemy = Instantiate(_tankPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

        }
    }
}
