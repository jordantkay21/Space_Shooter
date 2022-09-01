using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //4 requirments of a variable
        //1. public or private reference
            //public = outside world can communicate with this variable (other scripts/game objects)
            //private= ONLY the player object can communicate with this variable
        //2. data type (int, float, bool, string)
            //int = integer (whole numbers)
            //float = decimal values
            //bool = boolean (true/false)
            //string = character of text
        //3. every variable has a name
        //4.optional value assigned
    public float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
           
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
            //new vector3(1, 0, 0) | Time.deltaTime = 1second
            //new vector3(1, 0, 0) * 5 * real time (per second)
                //above is the same as new vector3(5, 0, 0) * Time.deltaTime
        //transform.Translate(Vector3.right * 5 * Time.deltaTime);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
