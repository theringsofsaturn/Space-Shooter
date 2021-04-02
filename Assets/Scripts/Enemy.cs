using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if bottom of screen, respawn at top with a new random x position
        if(transform.position.y < -6)
        {
            float randomX = Random.Range(9.0f, -9.0f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
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

            //destroy us
            Destroy(this.gameObject);
        }

        //if other is laser
        if (other.tag == "Laser")
        {
            //destroy laser
            Destroy(other.gameObject);
            //dstroy us
            Destroy(this.gameObject);

        }

    }
}
