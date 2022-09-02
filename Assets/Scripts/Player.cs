using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
        [SerializeField] //Lets designers modify values within the Unity Inspector when variables are private
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
           
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //new vector3(1, 0, 0) | Time.deltaTime = 1second
        //new vector3(1, 0, 0) * 5 * real time (per second)
        //above is the same as new vector3(5, 0, 0) * Time.deltaTime
        //transform.Translate(Vector3.right * 5 * Time.deltaTime);

        //new Vector3([-1 "left" or 1 "right"], 0, 0) * [-1 "left" or 0 "stay still" or "right"] * 3.5f * real time (1 second)
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
