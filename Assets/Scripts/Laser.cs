using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;
    private bool _isBehindShot = false;

    private EnemySniper _enemySniper;
    private Transform _enemySniperTransform;
    private Vector3 _enemySniperPos;
    private float _xAxis;
    private float _enemySniperXAxis;


    private void Start()
    {
        _enemySniper = GameObject.Find("Enemy_Sniper").GetComponent<EnemySniper>();
        _enemySniperTransform = GameObject.Find("Enemy_Sniper").GetComponent<Transform>();

        if (_enemySniper == null)
        {
            Debug.LogError("Enemy Sniper is NULL.");
        }

        _enemySniperPos = _enemySniperTransform.position;
        _xAxis = gameObject.transform.position.x;
        _enemySniperXAxis = _enemySniperPos.x;

    }

    private void Update()
    {
        if (_isEnemyLaser == false && _isBehindShot == false)
        {
            PlayerShotMovement();
            MoveSniper();
        }
        else if (_isEnemyLaser == true && _isBehindShot == false)
        {
            EnemyShotMovement();  
        }
        else
        {
            BehindShot();
        }
    }

    void MoveSniper()
    {
        float distance = Vector3.Distance(transform.position, _enemySniperPos);
        Debug.Log(distance);
        if(_xAxis < _enemySniperXAxis && distance < 3)
        {
            _enemySniper.SetDodgeRight();
        } 
        else if(_xAxis > _enemySniperXAxis && distance < 3)
        {
            _enemySniper.SetDodgeLeft();
        }
    }

    void PlayerShotMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    void BehindShot()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    void EnemyShotMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public void AssignBehindShot()
    {
        _isBehindShot = true;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser == true || _isBehindShot == true)
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
        }
    }
    

}
