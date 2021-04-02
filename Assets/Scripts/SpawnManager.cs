using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private bool _stopSpawing = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
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
        while (_stopSpawing == false)
        {
            //position to spawn enemies
            Vector3 posToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
            //instantiate an object (in order to instantiate an object we have to have a reference to that object. Create a variable enemyprefab), so =>>
            //instantiate enemy prefab
            Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

            //when do we want to run this code again? = yield wait for 5 seconds
            yield return new WaitForSeconds(5.0f);
            // !!because we are in an infinite loop we'll never get here because we'll never exit the while loop, we'll do it all over again!! So.. =>>
            //START COROUTINE in Start void method!
            //two methods: StartCoroutine("SpawnRoutine); or more optimized and quick way: StartCoroutine(SpawnCoroutine());
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}
