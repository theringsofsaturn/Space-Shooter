using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float speedPowerUp = 3.0f;

    //ID for powerups
    // 0 = triple shots // 1 = speed // 2 = shields
    [SerializeField] 
    private int powerupID;

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
            //handle to the component we want and assign the handle to the component
            //** the component we assign the variable to, should always match the component we are looking for
            Player player = other.transform.GetComponent<Player>();
            //best practice to do before accesing, is to null check or we may crush game
            if (player != null)
            {
                //if powerup ID is 0
                //if (powerupID == 0)
                //{
                //    player.TripleShotActive();
                //}

                ////else if powerup ID is 1
                //else if (powerupID == 1)
                //{
                //    //play speed powerup
                //}


                ////else if powerup ID is 2
                //else if (powerupID == 2)
                //{
                //    //play shields powerup
                //}

                //The same as the if-else statement above but more efficient is switch (like for example we have 100 ID!
                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Collected shields");
                        break;
                    default:
                        Debug.Log("Default value");
                        break;

                }

            }

            //on collected, destroy this
            Destroy(this.gameObject);
        }
    }

}
