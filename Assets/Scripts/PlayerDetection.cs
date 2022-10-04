using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private EnemyBrute _enemyBrute;

    private void Start()
    {
        _enemyBrute = GameObject.Find("Enemy_Brute").GetComponent<EnemyBrute>();

        if (_enemyBrute == null)
        {
            Debug.LogError("Enemy Brute is NULL.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _enemyBrute.FireBehindShot();
        }
    }
}
