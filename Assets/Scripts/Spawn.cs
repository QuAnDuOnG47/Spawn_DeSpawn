using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn : MonoBehaviour
{
    public ObjectPool objectPool;
    public int spawnPerFrame = 50; // Giới hạn số lượng spawn mỗi frame
    public int totalToSpawn = 1000;
    public float despawnDelay = 5f;
    public float respawnDelay = 2f;

    private List<GameObject> activeObjects = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFrog());
    }

    IEnumerator SpawnFrog()
{
    int batchSize = 100;

    while (true)
    {
        for (int i = 0; i < objectPool.poolSize; i += batchSize)
        {
            for (int j = 0; j < batchSize && (i + j) < objectPool.poolSize; j++)
            {
                Vector3 pos = new Vector3(Random.Range(-40f, 40f), 0, Random.Range(-40f, 40f));
                objectPool.Spawn(pos);
            }
            yield return null;
        }

        yield return new WaitForSeconds(despawnDelay);
        objectPool.DespawnAll();
        yield return new WaitForSeconds(respawnDelay);
    }
}

}
