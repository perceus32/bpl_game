using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float xVel, startDelay, repeatDelay;
    public int random;
    public float random2, coinSpawnChance = 60; //should be between 0 to 100
    public GameObject spawnManager;
    private GameObject child, coin1, coin2, coin3;
    public bool isGameOver = false;

    public GameObject coinPrefab;
    //private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("Generate", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        //Debug.Log(isGameOver);
        GameObject MovingChild;
        for (int i = 0; i < transform.childCount; i++)
        {
            MovingChild = transform.GetChild(i).gameObject;
            MoveObstacle(MovingChild);
            if (MovingChild.transform.position.x < -50)
            {
                Destroy(MovingChild);

            }

        }

    }
    void Generate()
    {
        if (isGameOver) return;
        //Debug.Log("working");
        random = Random.Range(0, 2);
        //Debug.Log(random2);
        random2 = Random.Range(-4, 0);
        child = Instantiate(prefabs[random], new Vector3(spawnManager.transform.position.x, random2, -1f), Quaternion.identity);
        child.transform.parent = spawnManager.transform;

        float random_luck = Random.Range(0, 100);
        if (random_luck <= coinSpawnChance)
        {
            SpawnCoins(child.transform);
        }

    }

    void MoveObstacle(GameObject child)
    {
        child.transform.position += Vector3.left * xVel * Time.deltaTime;
        if (isGameOver) return;
    }
   
    public void GameOver()
    {
        isGameOver = true;
    }
   
    void SpawnCoins(Transform parent)
    {
        float random_x = Random.Range((float)0.5, 1);
        float random_y = Random.Range(2, 3);
        var spawnPosition = parent.transform.position;
        coin1 = Instantiate(coinPrefab, new Vector3(spawnPosition.x, spawnPosition.y + random_y, spawnPosition.z), Quaternion.identity);
        coin2 = Instantiate(coinPrefab, new Vector3(spawnPosition.x + 1, spawnPosition.y + random_y, spawnPosition.z), Quaternion.identity);
        //coin3 = Instantiate(coinPrefab, new Vector3(spawnPosition.x + 2, spawnPosition.y + random_y, spawnPosition.z), Quaternion.identity);
        coin1.transform.parent = parent;
        coin2.transform.parent = parent;
        //coin3.transform.parent = parent;

    }
}