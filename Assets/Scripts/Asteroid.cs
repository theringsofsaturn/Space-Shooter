using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 19.0f;

    [SerializeField]
    private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on the zed axis
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        
        
        //destroy explosion after 3 seconds
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check for laser collision
        if (other.tag == "Laser")
        {
            //instantiate the asteroid at the position of the asteroid (us)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.25f);
        }
    }
}
