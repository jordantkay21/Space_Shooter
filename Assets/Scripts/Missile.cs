using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6;



    private Transform _enemy;
    private Transform _enemySniper;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        _enemySniper = GameObject.FindWithTag("Enemy_Sniper").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsEnemy();
    }

    private void MoveTowardsEnemy()
    {

        if (_enemySniper != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemySniper.position, Time.deltaTime * _speed);

            Vector3 offset = transform.position + _enemySniper.position;

            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), offset);
        }
        else if (_enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _enemy.position, Time.deltaTime * _speed);

            Vector3 offset = transform.position + _enemy.position;

            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1), offset);
        }
        else if (_enemy == null && _enemySniper == null)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);

            if (transform.position.y >= 8)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
