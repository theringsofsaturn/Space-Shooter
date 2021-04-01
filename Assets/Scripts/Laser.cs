﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speedLaser = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    { 
        //Translate laser position up * speed * real time
        transform.Translate(Vector3.up * _speedLaser * Time.deltaTime);

        //When laser is greater than 8, destroy the laser
        if (transform.position.y > 8)
        {
            Destroy(this.gameObject);
        }
    }
}