using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 19.0f;

    [SerializeField]
    private GameObject explosionPrefab;

    //handle to SpawnManager script
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign SpawnManager component
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on the zed axis
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //check for laser collision
        if (other.tag == "Laser")
        {
            //instantiate the asteroid at the position of the asteroid (us)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //destroy laser
            Destroy(other.gameObject);

            //before we destroy this gameObject (the asteroid), communicate with SpawnManager script and begin the instantiation for all the stuff
            _spawnManager.StartSpawning();

            //destroy asteroid
            Destroy(this.gameObject, 0.25f);
        }
    }
}
