using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 19.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on the zed axis
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        //check for laser collision
        //instantiate the asteroid at the position of the asteroid (us)
        //destroy explosion after 3 seconds
    }
}
