using UnityEngine;
using System.Collections.Generic;

public class objectPool : MonoBehaviour
{
   public GameObject prefab; 
    public  int poolSize = 1000; 
    private List<GameObject> pool;
    void Awake()
    {
        pool = new List<GameObject>(poolSize);
        for(int i = 0 ; i<poolSize ; i++){
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false); 
            pool.Add(obj);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SpawnForg(Vector3 position){
       foreach(var obj in pool){
           if(!obj.activeInHierarchy){
               obj.transform.position = position;
               obj.SetActive(true);
               return obj;
           }
       }
       Debug.LogWarning("Không còn ếch trong pool!");
       return null;
    }

    public void DespawnAll()
    {
        foreach (var fish in pool)
        {
            fish.SetActive(false);
        }
    }
}
