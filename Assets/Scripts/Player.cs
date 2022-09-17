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
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    [SerializeField]
    private int _ammoCount = 30;

    //Variable that hold a GameObject
    [SerializeField]
    private GameObject _laserPrefab;
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
    

    //Variables that hold a Vector3
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0, .8f, 0);
       
   //Bool Variables
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;

 
    // Start is called before the first frame update
    void Start()
    {
           

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        
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
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser(); ;
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
            transform.Translate(direction * _thruster * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
       


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

 
        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0); ;
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
            if(_ammoCount == 0)
            {
                return; 
            }
            else
            {
                _ammoCount--;
                Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
                _audioSource.Play();
                _uiManager.UpdateAmmoCount(_ammoCount);
            }        
        }  
    }

    public void RefillAmmo()
    {
        _ammoCount = 30;
        _uiManager.UpdateAmmoCount(_ammoCount);
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
            ShieldDeactivate();
            return;
        }
        
        _lives--;

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
