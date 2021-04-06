using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speedLaser = 8.0f;

    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    { 
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp() //if it's a player laser move up
    {
        //Translate laser position up * speed * real time
        transform.Translate(Vector3.up * _speedLaser * Time.deltaTime);

        //When laser is greater than 8, destroy the laser
        if (transform.position.y > 8)
        {
            //check if object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy the parent too
            Destroy(this.gameObject);
        }
    }

    void MoveDown() //if it's an enemy laser move down
    {
        //Translate laser position up * speed * real time
        transform.Translate(Vector3.down * _speedLaser * Time.deltaTime);

        //When laser is greater than 8, destroy the laser
        if (transform.position.y < -8)
        {
            //check if object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy the parent too
            Destroy(this.gameObject);
        }
    }
}
