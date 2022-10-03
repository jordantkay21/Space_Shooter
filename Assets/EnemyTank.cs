using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _atttackSpeed = 12.0f;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private Transform _playerTransform;

    private Vector3 _playerPos;

    private bool _stopFire = false;
    private bool _missPlayer = false;



    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
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

        _playerPos = _playerTransform.position;
    }

    void Update()
    {
        if (_missPlayer == false)
        {
            FlyToPlayer();
        }

        if (_missPlayer == true)
        {
            CalculateMovement();
            FireLaser();
        }

    }
    void FireLaser()
    {
        if (Time.time > _canFire && _stopFire == false)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();

            }
        }
    }

    void FlyToPlayer()
    {
        float step = _atttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _playerPos, step);
        if (transform.position == _playerPos)
        {
            _missPlayer = true;
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

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
                _player.AddScore(10);
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

        if (other.tag == "Missile")
        {
            if (_player != null)
            {
                _player.AddScore(10);
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
