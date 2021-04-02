using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn object every 5 seconds
    //create a coroutine of type IEnumerator -- it allowes us to yield events
    

    IEnumerator SpawnRoutine()
    {
        //use while loop (infinite loop)
        while (true)
        {
            //position to spawn enemies
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
            Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
        }

            //instantiate an object (in order to instantiate an object we have to have a reference to that object. Create a variable), so =>>
            //instantiate enemy prefab
            //when do we want to run this code again? = yield wait for 5 seconds
    }
}
