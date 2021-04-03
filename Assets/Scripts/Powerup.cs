using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speedPowerUp = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down at the speed of 3
        transform.Translate(Vector3.down * speedPowerUp * Time.deltaTime);

        //when leaving the screen destroy this object
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //only be collectable by the player
        if (other.tag == "Player")
        {
            //communicate with player script. Create a variable player to have a reference of the component we want. Handle to the component we want
            //(other => in this case is Player, we don't need to find it, we already know wehere it is (in 'other' we collide) and assign the handle to the component
            //handle to the component I want and assign the handle to the component
            //** the component we assign the variable to, should always match the component we are looking for
            Player player = other.transform.GetComponent<Player>();
            //best practice to do before accesing, is to null check or we may crush game
            if (player != null)
            {
                player.TripleShotActive();
            }

            //on collected destroy
            Destroy(this.gameObject);
        }
    }

}
