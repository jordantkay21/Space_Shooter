using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [Header("Laser Prefabs")]  
    [SerializeField]
    private bool _isShooting = false;
    [SerializeField]
    private float _gunTemp = .6f;
    [SerializeField]
    private GameObject _greenLaserPrefab, _orangeLaserPrefab, _redLaserPrefab;


    [SerializeField]
    private float _fireRate = .30f;
    [SerializeField]
    private float _speed = 3;
    private float _canFire = -1;


    private Transform _playerTransform;

    private float _playerXAxis; 
    private Vector3 _startPos; 
    private Vector3 _targetPos;

    private bool _letItRain;



    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        
        if (_playerTransform == null)
        {
            Debug.LogError("Player is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _canFire)
        {
            FireLaser();
        }
        CalculateXAxis();
        CalculateMovement();
    }

private void CalculateXAxis()
    {
        _playerXAxis = _playerTransform.position.x;

        if (_playerXAxis < -5.5f)
        {
            _playerXAxis = -5.5f;
        }
        else if (_playerXAxis > 6)
        {
            _playerXAxis = 6;
        }
    }

    private void CalculateMovement()
    {
        if (_isShooting == false)
        {
            transform.position = new Vector3(_playerXAxis, 6, 0);
        }
        else
        {
            return;
        }

    }
    
    public void StartShooting()
    {
        _isShooting = true;
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isShooting == true)
        {
            _gunTemp -= Time.deltaTime;

            if (.6f > _gunTemp && _gunTemp > .4f)
            {
                GameObject greenMothershipLaser = Instantiate(_greenLaserPrefab, transform.position, Quaternion.identity);
                Laser greenLaser = greenMothershipLaser.GetComponent<Laser>();
                greenLaser.AssignEnemyLaser();
            }
            else if (.4f > _gunTemp && _gunTemp > .2f)
            {
                GameObject orangeMothershipLaser = Instantiate(_orangeLaserPrefab, transform.position, Quaternion.identity);
                Laser orangeLaser = orangeMothershipLaser.GetComponent<Laser>();
                orangeLaser.AssignEnemyLaser();
            }
            else if (.2f > _gunTemp && _gunTemp > 0)
            {
                GameObject redSniperLaser = Instantiate(_redLaserPrefab, transform.position, Quaternion.identity);
                Laser redLaser = redSniperLaser.GetComponent<Laser>();
                redLaser.AssignEnemyLaser();
            }
        }

        if(_gunTemp < 0)
        {
            _gunTemp = 0;
        }
        if (_gunTemp == 0)
        {
            _isShooting = false;
            _gunTemp = .6f;
        }


    }
}
