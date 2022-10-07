using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    [Header("Holds a Value")]
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _health = 100;


    private Player _player;
    private Transform _playerTransform;
    private AudioSource _audioSource;
    private LaserGun _laserGun;
    private UIManager _uiManager;
    private TankLauncher _tankLauncher;
    private SniperLauncher _sniperLauncher;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
        _laserGun = GetComponentInChildren<LaserGun>();
        _tankLauncher = GetComponentInChildren<TankLauncher>();
        _sniperLauncher = GetComponentInChildren<SniperLauncher>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Enemy Audio Source is NULL.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        if(_laserGun == null)
        {
            Debug.LogError("LaserGun is NULL.");
        }
        if(_tankLauncher == null)
        {
            Debug.LogError("TankLauncher is NULL.");
        }
        if(_sniperLauncher == null)
        {
            Debug.LogError("SniperLauncher is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthCheck();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _health -= 1;
            _laserGun.StartShooting();

        }

        if (other.tag == "Missile")
        {
            Destroy(other.gameObject);
            _health -= 5;

        }
    }

   
    public void HealthCheck()
    {
       if (_health == 100)
        {
            _uiManager.UpdateBossHealth(20);
        } 
       else if (_health == 95)
        {
            _uiManager.UpdateBossHealth(19);
        }
        else if (_health == 90)
        {
            _uiManager.UpdateBossHealth(18);
        }
        else if (_health == 85)
        {
            _uiManager.UpdateBossHealth(17);
        }
        else if (_health == 80)
        {
            _uiManager.UpdateBossHealth(16);
        }
        else if (_health == 75)
        {
            _uiManager.UpdateBossHealth(15);
            _tankLauncher.SetFireTank();
        }
        else if (_health == 70)
        {
            _uiManager.UpdateBossHealth(14);
        }
        else if (_health == 65)
        {
            _uiManager.UpdateBossHealth(13);
        }
        else if (_health == 60)
        {
            _uiManager.UpdateBossHealth(12);
        }
        else if (_health == 55)
        {
            _uiManager.UpdateBossHealth(11);
        }
        else if (_health == 50)
        {
            _uiManager.UpdateBossHealth(10);
            _sniperLauncher.SetFireSniper();
            
        }
        else if (_health == 45)
        {
            _uiManager.UpdateBossHealth(9);
        }
        else if (_health == 40)
        {
            _uiManager.UpdateBossHealth(8);
        }
        else if (_health == 35)
        {
            _uiManager.UpdateBossHealth(7);
        }
        else if (_health == 30)
        {
            _uiManager.UpdateBossHealth(6);
        }
        else if (_health == 25)
        {
            _uiManager.UpdateBossHealth(5);
        }
        else if (_health == 20)
        {
            _uiManager.UpdateBossHealth(4);
        }
        else if (_health == 15)
        {
            _uiManager.UpdateBossHealth(3);
        }
        else if (_health == 10)
        {
            _uiManager.UpdateBossHealth(2);
        }
        else if (_health == 5)
        {
            _uiManager.UpdateBossHealth(1);
        }
        else if (_health == 0)
        {
            _uiManager.UpdateBossHealth(0);
            _uiManager.YouWinSequence();
        }

    }
 

}
