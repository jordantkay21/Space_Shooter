using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperVision : MonoBehaviour
{

    private EnemySniper _enemySniper;

    // Start is called before the first frame update
    void Start()
    {
        _enemySniper = GetComponentInParent<EnemySniper>();


        if (_enemySniper == null)
        {
            Debug.LogError("Enemy Sniper is NULL.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Powerup")
        {
            _enemySniper.ShootPowerup();
        }
    }
}
