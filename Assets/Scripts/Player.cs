using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables that hold a value
    [SerializeField] 
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedBoostPowerup = 10.0f;
    [SerializeField]
    private float _thruster = 7.0f;
    [SerializeField]
    private float _thrusterTimer = 15f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _shieldLives = 0;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _laserAmmoCount = 15;
    [SerializeField]
    private int _missileAmmoCount = 0;

    //Variable that hold a GameObject
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _missilePrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftDamage, _rightDamage;
    [SerializeField]
    private AudioClip _laserSoundClip, _powerUpClip;
    private AudioSource _audioSource;
    private CameraShake _camShake;

    

    //Variables that hold a Vector3
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, .8f, 0);
       
   //Bool Variables
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private bool _isThrusterScaleActive = false;
    private bool _isThrusterScaleReloading = false;

 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrusterScaleUp());
           

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _camShake = GameObject.Find("Main_Camera").GetComponent<CameraShake>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        
        if (_audioSource == null)
        {
            Debug.LogError("The Laser Audio on the Player is NULL");
        }

        if (_camShake == null)
        {
            Debug.LogError("The Camera Script on the Player is NULL");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser(); 
        }

        if (Input.GetKeyDown(KeyCode.M) && Time.time > _canFire)
        {
            FireMissile();
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction * _speedBoostPowerup * Time.deltaTime);
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _isThrusterScaleReloading = true;
            if (_thrusterTimer > 0)
            {
                transform.Translate(direction * _thruster * Time.deltaTime);
                if (_isThrusterScaleActive == false)
                {
                    StartCoroutine(ThrusterScaleDown());
                }
            }
            else
            {
                transform.Translate(direction * _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isThrusterScaleActive = false;
            _isThrusterScaleReloading = false;
        }

        if (_isThrusterScaleReloading == false)
        {
            StartCoroutine(ThrusterScaleUp());
        }

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0); ;
        }
    }
    IEnumerator ThrusterScaleDown()
    {
        _isThrusterScaleActive = true;
        while (_thrusterTimer > 0 && _isThrusterScaleActive == true)
        {
            yield return new WaitForEndOfFrame();
            _thrusterTimer -= Time.deltaTime;
            _uiManager.UpdateThrusterScale((int)_thrusterTimer);
        } 
        if(_thrusterTimer < 0)
        {
            _thrusterTimer = 0;
        }
    }
    IEnumerator ThrusterScaleUp()
    {
        _isThrusterScaleReloading = true;
        while (_thrusterTimer < 15 && _isThrusterScaleReloading == true)
        {
            yield return new WaitForSeconds(5);
            _thrusterTimer += 1;
            if (_thrusterTimer > 15)
            {
                _thrusterTimer = 15;
            }
            _uiManager.UpdateThrusterScale((int)_thrusterTimer);
        }

        if (_thrusterTimer == 15)
        {
            _isThrusterScaleReloading = false;
        }

    }

    public void EmptyThrusters()
    {
        StartCoroutine(EmptyThrusterRoutine());
    }

    public IEnumerator EmptyThrusterRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (AreThrustersFull() == true)
        {
            _thrusterTimer = 0;
            _uiManager.UpdateThrusterScale((int)_thrusterTimer);
        }
    }

    public bool AreThrustersFull()
    {
        if(_thrusterTimer == 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        _audioSource.clip = _laserSoundClip;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            if(_laserAmmoCount == 0)
            {
                return; 
            }
            else
            {
                _laserAmmoCount--;
                Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
                _audioSource.Play();
                _uiManager.UpdateAmmoCount(_laserAmmoCount);
            }        
        }  
    }

    void FireMissile()
    {
        if(_missileAmmoCount == 0)
        {
            return;
        }
        else
        {
            _missileAmmoCount--;
            Instantiate(_missilePrefab, transform.position + _laserOffset, Quaternion.identity);
            _uiManager.UpdateMissileAmmo(_missileAmmoCount);
        }
    }

    public void RefillAmmo()
    {
        PowerUpSound();
        _laserAmmoCount = 15;
        _uiManager.UpdateAmmoCount(_laserAmmoCount);
    }

    public void RefillMissile()
    {
        PowerUpSound();
        if(_missileAmmoCount < 3)
        {
            _missileAmmoCount++;
            _uiManager.UpdateMissileAmmo(_missileAmmoCount);
        }
        else
        {
            return;
        }
    }

    public void GiveLife()
    {
        PowerUpSound();
        if (_lives == 3)
        {
            return;
        }
        else
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            CheckLives();
        }
        
    }
    
    public void Damage()
    {

        if(_isShieldActive == true)
        {
            if(_shieldLives == 3)
            {
                _shieldLives--;
                _uiManager.UpdateShieldLives(_shieldLives);
                return;
            }
            else if(_shieldLives == 2)
            {
                _shieldLives--;
                _uiManager.UpdateShieldLives(_shieldLives);
                return;
            }
            else if(_shieldLives == 1)
            {
                _shieldLives--;
                _uiManager.UpdateShieldLives(_shieldLives);
                ShieldDeactivate();
                return;
            }
        }
        
        _lives--;
        _camShake.CameraShakeStart();

        _uiManager.UpdateLives(_lives);
        CheckLives();
    }

    private void CheckLives()
    {
        
        if (_lives == 3)
        {
            _leftDamage.gameObject.SetActive(false);
            _rightDamage.gameObject.SetActive(false);
        }
        else if (_lives == 2)
        {
            _leftDamage.gameObject.SetActive(true);
            _rightDamage.gameObject.SetActive(false);
        }
        else if (_lives == 1)
        {
            _leftDamage.gameObject.SetActive(true);
            _rightDamage.gameObject.SetActive(true);
        }

        if (_lives < 1)
        {
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
            _uiManager.GameOverSequence();
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        PowerUpSound();
        StartCoroutine(TripleShotPowerDownRoutine()); 
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        PowerUpSound();

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldLives = 3;
        _uiManager.UpdateShieldLives(_shieldLives);
        PowerUpSound();
        _shieldVisualizer.SetActive(true);
    }

    public void ShieldDeactivate()
    {
        _isShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    private void PowerUpSound()
    {
        _audioSource.clip = _powerUpClip;
        _audioSource.Play();
    }

    
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }






}
