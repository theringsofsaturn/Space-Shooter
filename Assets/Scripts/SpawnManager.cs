using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn object every 5 seconds
    //create a coroutine of type IEnumerator -- it allowes us to yield events
    IEnumerator SpawnEnemyRoutine()
    {
        //use while loop (infinite loop)
        while (_stopSpawning == false)
        {
            //position to spawn enemies
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);

            //instantiate an object (in order to instantiate an object we have to have a reference to that object. Create a variable enemyprefab), so =>>
            //instantiate enemy prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //if we want to hold onto the reference of the object so once that's instantiated, we can manipulated it, we need to store it into a game object of type variable
            //we create the variable (see above) of type Game Object called newenemy, so we can access it

            //when do we want to run this code again? = yield wait for 5 seconds
            yield return new WaitForSeconds(5.0f);
            // !!because we are in an infinite loop we'll never get here because we'll never exit the while loop, we'll do it all over again!! So.. =>>
            //START COROUTINE in Start void method!
            //two methods: StartCoroutine("SpawnRoutine); or more optimized and quick way: StartCoroutine(SpawnCoroutine());
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        //evry 3-7 seconds spawn in a powerup
        while (_stopSpawning == false)
        {
            //position to spawn powerups
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);

            //instantiate powerups array
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);

            //when do we want to run this code again? = yield wait 3-7 random seconds
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

    }
}
