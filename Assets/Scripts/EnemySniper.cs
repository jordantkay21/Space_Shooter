using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniper : MonoBehaviour
{
    private Vector3 _laserOffset = new Vector3(0, -1, 0);

    [SerializeField]
    private int _enemyScore = 30;
    [SerializeField]
    private float _waitBeforeMoving = 5.0f;
    [SerializeField]
    private GameObject _enemySniperLaserPrefab;
    [SerializeField]
    private float _speed = 12.0f;
    private float _fireRate = 2.0f;
    private float _canFire = -1;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;

    private bool _stopFire = false;
    private bool _hasArrived = false;
    private bool _atLocation;
    private bool _hasFired = false;
    private bool _dodgeRight = false;
    private bool _dodgeLeft = false;


    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL.");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Enemy Audio Source is NULL.");
        }


    }

    void Update()
    {
        Dodge();
        CalculateMovement();
        FireLaser();

    }

    public void SetDodgeRight()
    {
        _dodgeRight = true;
    }

    public void SetDodgeLeft()
    {
        _dodgeLeft = true;
    }

    private void Dodge()
    {
        _speed = 8f;
        if (_dodgeRight == true)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _dodgeRight = false;
        }
        else if (_dodgeLeft == true)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _dodgeLeft = false;
        }
    }


    private void CalculateMovement()
    {
        float xAxis = transform.position.x;

        if (_hasArrived == false)
        {
            float randX = Random.Range(-10, 10);
            float randY = Random.Range(1, 6);
            StartCoroutine(MoveToPoint(new Vector3(randX, randY, 0)));

            _hasArrived = true;
        }

        if (xAxis > 10)
        {
            transform.position = new Vector3((xAxis * -1), 0, 0);
        }
        if (xAxis < -10)
        {
            transform.position = new Vector3((xAxis * -1), 0, 0);
        }
    }

    IEnumerator MoveToPoint(Vector3 targetPos)
    {
        yield return new WaitForSeconds(_waitBeforeMoving);

        _atLocation = false;
        _hasFired = false;

        while (_atLocation == false)
        {
            Vector3 startPos = transform.position;
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(startPos, targetPos, Time.deltaTime * _speed);

            if (Vector3.Distance(transform.position, targetPos) == 0)
            {
                _atLocation = true;
            }
        }

        yield return new WaitForSeconds(2);


        _hasArrived = false;

    }

    public void ShootPowerup()
    {
        GameObject enemySniperLaser = Instantiate(_enemySniperLaserPrefab, transform.position + _laserOffset, Quaternion.identity);
        Laser laser = enemySniperLaser.GetComponent<Laser>();
        laser.AssignEnemyLaser();
    }
    void FireLaser()
    {
        if (_atLocation == true && _hasFired == false)
        {
            _canFire = Time.time + _fireRate;
            GameObject enemySniperLaser = Instantiate(_enemySniperLaserPrefab, transform.position + _laserOffset, Quaternion.identity);
            Laser laser = enemySniperLaser.GetComponent<Laser>();
            laser.AssignEnemyLaser();

            _hasFired = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Missile")
        {
            if (_player != null)
            {
                _player.AddScore(_enemyScore);
                _player.UpdateKillCount(1);
            }

            Destroy(other.gameObject);

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            _stopFire = true;
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this.gameObject, 2.6f);
        }

        if (other.tag == "Laser")
        {
            if (_player != null)
            {
                _player.AddScore(_enemyScore + 10);
                _player.UpdateKillCount(1);
            }

            Destroy(other.gameObject);

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            _stopFire = true;
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this.gameObject, 2.6f);


        }
    }

}
