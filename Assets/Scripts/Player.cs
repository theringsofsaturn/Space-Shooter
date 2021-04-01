using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private reference
    //data type (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField]
    private float _speed = 3.5f;
    public GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //if I hit the space key, spawn game object
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new Vector3(1, 0, 0) * 0 * 3.5 * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //new Vector3(0, 1, 0) * 0 * 3.5 * real time
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //if player position on the y is greater than 6, y position = 6
        //else if position on the y is less than -4, y position = -4

        if (transform.position.y >= 6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
        else if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(transform.position.x, -4f, 0);
        }

        //if player position x is greater than 9.2, position x = 9.2
        //else if position x less than -9.2, position x = -9.2

        if (transform.position.x >= 9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }
    }
}
