using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private GameObject _enemy;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
             
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is Player
        //Destroy Us

        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        //if other is Laser
        //Destroy Laser
        //Destroy Us

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
