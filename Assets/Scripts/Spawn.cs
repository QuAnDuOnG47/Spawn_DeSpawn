using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public objectPool objectPool; 
     public float delayBeforeDespawn = 5f;
    public float delayBeforeRespawn = 2f;

    void Start()
    {
        StartCoroutine(SpawnFrog());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnFrog()
    {
        while (true)
        {
        for (int i = 0; i < objectPool.poolSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-40f, 40f), 0, Random.Range(-40f, 40f));
            objectPool.SpawnForg(pos);
            yield return null; 
        }
        
        yield return new WaitForSeconds(delayBeforeDespawn);
        objectPool.DespawnAll();
        yield return new WaitForSeconds(delayBeforeRespawn);
        }
    }
}
