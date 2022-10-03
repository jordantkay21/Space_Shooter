using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperView : MonoBehaviour
{
    private EnemySniper _enemySniper;
    void Start()
    {
        _enemySniper = GameObject.Find("Enemy_Sniper").GetComponent<EnemySniper>();

        if(_enemySniper == null)
        {
            Debug.LogError("Enemy Sniper is NULL.");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Sniper")
        {
            _enemySniper.ShootPowerup();
        }
    }


}
