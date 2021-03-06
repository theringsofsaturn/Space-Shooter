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
    private int _lives = 3;

    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _leftEngine, _rightEngine;

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;

    //communicate with SpawnManager script to call void Damage(). Create a variable spawnManager to have a reference of the component we want and then assign it in void Start
    private SpawnManager _spawnManager;

    //global references. we create a handle to the components we want
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign variable
        //find the Game Object Spawn Manager. Then get component SpawnManager
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //assign audio source 
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

        if (_uiManager == null)
        {
            Debug.Log("The UI Manager is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the player is NULL");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        
        //take the current position = new position (position of the player when the game starts)
        transform.position = new Vector3(0, 0, 0);
        
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
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        }
        //else fire 1 laser
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        }

        //play the laser audio clip
        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        //all the three are exactly the same
        //_lives = _lives - 1;
        //_lives -= 1;
        _lives --;

        if (_lives == 2)
        {
            //enable left engine
            _leftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            //enable right engine
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

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
        _shieldVisualizer.SetActive(true);
    }

    //method to add 10 the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
