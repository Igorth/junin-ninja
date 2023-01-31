using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cat;

    public float min_X = -2.5f, max_X = 2.5f;

    public float spawnTime_Min = 0.5f, spawnTime_Max = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCat());
    }

    // Dellay to do the execution
    IEnumerator SpawnCat()
    {
        yield return new WaitForSeconds(Random.Range(spawnTime_Min, spawnTime_Max));

        GameObject c = Instantiate(cat);

        float x = Random.Range(min_X, max_X);

        c.transform.position = new Vector2(x, transform.position.y);

        StartCoroutine(SpawnCat());

    }
}
