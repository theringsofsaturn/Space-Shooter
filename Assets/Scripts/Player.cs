using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {                     //new Vector3(5, 0, 0) * 5 * real time
        transform.Translate(Vector3.right * 5 * Time.deltaTime);
    }
}
