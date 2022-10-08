using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperLauncher : MonoBehaviour
{
    [Header("Sniper Tank Prefab")]
    [SerializeField]
    private bool _fireSniper;
    [SerializeField]
    private GameObject _sniperPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private void Start()
    {

    }

    public void SetFireSniper()
    {
        _fireSniper = true;        
        StartCoroutine(SpawnSniperRoutine());
    }

    public void FireTankFalse()
    {
        _fireSniper = false;
    }
    IEnumerator SpawnSniperRoutine()
    {
        yield return new WaitForEndOfFrame();
        while (_fireSniper == true)
        {
            yield return new WaitForSeconds(Random.Range(4.0f, 10.0f));
            Vector3 posToSpawn = transform.position;
            GameObject newEnemy = Instantiate(_sniperPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

        }
    }
}
