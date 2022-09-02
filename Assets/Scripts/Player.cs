using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
        [SerializeField] 
    private float _speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
           

        transform.position = new Vector3(0, 0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        //if player position on the y is greatter than 0
        //than y position = 0
        //else if position on the y is less than -3.8f
        //y pos = -3.8f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0); 
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //if player position on the x is greater than 11.3f
        //than x position = -11.3f 
        //else if player position on the x is less than -11.33f
        //than pos = 11.3f

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0); ;
        }

    }
}
