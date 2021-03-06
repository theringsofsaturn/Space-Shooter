using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

    public GameObject enemyPrefab;

    [SerializeField]
    private GameObject _laserPrefab;

    private AudioSource _audioSource;

    //global references. we create a handle to the components we want
    private Player _player;

    //handle to the animator component
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        //this way we assign one time and chache the component player and use it freely throughout the entire script. This because using GetComponent many times is expensive
        _player = GameObject.Find("Player").GetComponent<Player>();
        //assign audio source component
        _audioSource = GetComponent<AudioSource>();

        //null check player
        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }

        //assign the component animator
        _anim = GetComponent<Animator>();

        //null check animator
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            //create an array "lasers" and assign it to the components of the childrens (lasers) of the game object Enemy Laser which have the component Laser.cs
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            
            //index the lasers and call the method AssignEnemyLaser() in Laser.cs
            for (int i = 0; i < lasers.Length; ++i )
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen, respawn at top with a new random x position
        if (transform.position.y < -6)
        {
            float randomX = Random.Range(9.0f, -9.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player 
        if (other.tag == "Player")
        {
            //damage player

            //to avoid errors like "NullReferenceException, stored 'Player' in a reference variable
            Player player = other.transform.GetComponent<Player>();
            
            if (player != null)
            {
                player.Damage();
            }

            //trigger anim
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;

            //play explosion clip
            _audioSource.Play();

            //destroy us
            Destroy(this.gameObject, 2.7f);
        }

        //if other is laser
        if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);

            //check if player is alive
            if (_player != null)
            {
                _player.AddScore(1);
            }

            //trigger anim
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;

            //play expplosion clip
            _audioSource.Play();

            //destory collider (to prevent double shooting with double explosion and audio)
            Destroy(GetComponent<Collider2D>());

            //destroy us
            Destroy(this.gameObject, 2.7f);
        }

    }
}
