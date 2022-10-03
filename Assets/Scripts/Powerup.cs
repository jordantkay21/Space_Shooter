using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int _powerupID; // 0=Tripleshot | 1=Speed | 2=Shield |3=+3 Ammo | 4=Health | 5=Missile | 6=Drain | 7=Rapid Fire 


    private Transform _playerTransform;
    private Player _playerScript;



    private void Start()
    {
        _playerTransform = GameObject.Find("Player").GetComponent <Transform> ();
        _playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }

        CollectPowerups();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();                   
                        break;
                    case 3:
                        player.AddThreeToAmmo();
                        break;
                    case 4:
                        player.GiveLife();
                        break;
                    case 5:
                        player.RefillMissile();
                        break;
                    case 6:
                        player.EmptyThrustersPowerdown();
                        break;
                    case 7:
                        player.RapidFireActivate();
                        break;
                }
            }                     
            Destroy(this.gameObject);
        }
    }

    public void MoveToPlayer()
    {
        _speed = 10.0f;
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, step);
    }

    private void CollectPowerups()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (_playerScript.AreThrustersFull() == true)
            {
                _playerScript.EmptyThrusters();
                MoveToPlayer();
            }
            else
            {
                return;
            }
        }

    }
}
