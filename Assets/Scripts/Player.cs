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
    private float speedMultiplier = 2;
    private float _firerate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private float _lives = 3.0f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    
    //communicate with SpawnManager script to call void Damage(). Create a variable spawnManager to have a reference of the component we want and then assign it in void Start
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign variable
        //find the Game Object Spawn Manager. Then get component SpawnManager
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        //before accesing it, check if SpawnManager is succesfully grabbed or is null
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

        
        //take the current position = new position (position of the player when the game starts)
        transform.position = new Vector3(0, -2.0f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //if I hit the space key, spawn laser game object
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    //player movement
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new Vector3(1, 0, 0) * 0 * 3.5 * real time == to =>>
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);

        //new Vector3(0, 1, 0) * 0 * 3.5 * real time == to =>>
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

    //fire laser method
    void FireLaser()
    {
        _canFire = Time.time + _firerate;

        if (_isTripleShotActive == true)
        {
            //fire 3 lasers (triple shot prefab)
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.837f, -1.55f, -0f), Quaternion.identity);
        }
        //else fire 1 laser
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        }

    }

    public void Damage()
    {
        if (_isShieldActive = true)
        {
            _isShieldActive = false;
            return;
        }

        //all the three are exactly the same
        //_lives = _lives - 1;
        //_lives -= 1;
        _lives --;

        //check if dead
        if (_lives < 1)
        {
            //communicate with SpawnManager script. let them know to stop spawing
            _spawnManager.OnPlayerDeath();

            //destroy us
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        //TripleShotActive becomes true
        _isTripleShotActive = true;
        //start coroutine
        StartCoroutine(TripleShotPowerDownRoutine());
        
    }

    //start the power down coroutine for triple shot
    IEnumerator TripleShotPowerDownRoutine()
    {
        //wait 5 seconds
        yield return new WaitForSeconds(5.0f);
        //set triple shot to false
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= speedMultiplier;

        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;

        _speed /= speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
    }
}
