using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrute : MonoBehaviour
{


    [SerializeField]
    private float _speed = 4.0f;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [Header("Brutes Components")]
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private GameObject _enemyLaserPrefab;
    [SerializeField]
    private GameObject _behindLaserPrefab;

    private bool _stopFire = false;
    private bool _isShieldActive = true;
    private bool _isPassThroughActive = false;
    private bool _hasFired;



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
        CalculateMovement();
        FireLaser();

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

    public void FireBehindShot()
    {

        if (_hasFired == false)
        {
            _canFire = Time.time + _fireRate;
            GameObject behindLaser = Instantiate(_behindLaserPrefab, transform.position, Quaternion.identity);
            Laser laser = behindLaser.GetComponent<Laser>();
            laser.AssignBehindShot();
        }

        _hasFired = true;
        StartCoroutine(HasFiredFalse());
    }

    IEnumerator HasFiredFalse()
    {
        yield return new WaitForSeconds(1);
        _hasFired = false;
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0);
        }
    }

    IEnumerator PassThroughEffectActive()
    {

        Rigidbody2D rigidBody = this.GetComponent<Rigidbody2D>();

        yield return new WaitForEndOfFrame();
        _isPassThroughActive = true;
        StartCoroutine(PassThroughEffectDeactivated());


        while (_isPassThroughActive == true)
        {
            _collider.enabled = false;

            _renderer.enabled = false;
            yield return new WaitForSeconds(.2f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(.2f);


        }
    }

    IEnumerator PassThroughEffectDeactivated()
    {
        yield return new WaitForSeconds(2);
        _isPassThroughActive = false;
        _collider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (_isShieldActive == true)
            {
                if (player != null)
                {
                    player.Damage();
                }
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
                StartCoroutine(PassThroughEffectActive());
            }
            else
            {
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

        }

        if (other.tag == "Laser")
        {
            Player player = other.transform.GetComponent<Player>();

            if (_isShieldActive == true)
            {
                if (player != null)
                {
                    player.Damage();
                }
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
                StartCoroutine(PassThroughEffectActive());
            }
            else
            {
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
        }

        if (other.tag == "Missile")
        {
            Player player = other.transform.GetComponent<Player>();

            if (_isShieldActive == true)
            {
                if (player != null)
                {
                    player.Damage();
                }
                _isShieldActive = false;
                _shieldVisualizer.SetActive(false);
                StartCoroutine(PassThroughEffectActive());
            }
            else
            {
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


        }
    }

    

}
