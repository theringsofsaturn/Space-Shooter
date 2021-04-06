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

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if we (laser) collides with the player and the laser is Enemy's laser
        if (other.tag == "Player" & _isEnemyLaser == true)
        {
            //reference to the player script int he Player game object, in order to access the method Damage()
            Player player = other.GetComponent<Player>();

            //check null (we don't want to call it if it's not the player) - meaning a player component (Player.cs) exist on the object we hit or collide with
            if (player != null)
            {
                player.Damage();
            }
        }
    }
}
