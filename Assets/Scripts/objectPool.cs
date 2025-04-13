using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 1000;
    
    private Queue<GameObject> pool;
    private List<GameObject> activeObjects; // Theo dõi các object đang active

    void Awake()
    {
        pool = new Queue<GameObject>(poolSize);
        activeObjects = new List<GameObject>(poolSize);
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Spawn(Vector3 position)
    {
        if (pool.Count == 0)
        {
            // Tái sử dụng object active lâu nhất thay vì Instantiate mới
            return RecycleOldestActiveObject(position);
        }
        
        GameObject obj = pool.Dequeue();
        SetupObject(obj, position);
        activeObjects.Add(obj);
        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        activeObjects.Remove(obj);
        pool.Enqueue(obj);
    }

    public void DespawnAll()
    {
        // Tạo bản sao của danh sách để tránh modify while iterating
        List<GameObject> objectsToDespawn = new List<GameObject>(activeObjects);
        foreach (var obj in objectsToDespawn)
        {
            Despawn(obj);
        }
    }

    // ===== Các phương thức hỗ trợ =====
    private GameObject RecycleOldestActiveObject(Vector3 position)
    {
        if (activeObjects.Count == 0)
        {
            Debug.LogError("No objects available in pool and no active objects to recycle!");
            return null;
        }

        // Tìm object active lâu nhất
        GameObject oldestActive = activeObjects[0];
        float oldestTime = Time.time;
        
        foreach (var obj in activeObjects)
        {
            if (obj.activeSelf && obj.GetComponent<PooledObject>().spawnTime < oldestTime)
            {
                oldestActive = obj;
                oldestTime = obj.GetComponent<PooledObject>().spawnTime;
            }
        }

        // Despawn và spawn lại
        Despawn(oldestActive);
        return Spawn(position); // Gọi đệ quy - lúc này pool đã có object
    }

    private void SetupObject(GameObject obj, Vector3 position)
    {
        obj.transform.position = position;
        obj.SetActive(true);
        
        // Ghi nhận thời gian spawn (cần thêm component PooledObject)
        var pooledObj = obj.GetComponent<PooledObject>();
        if (pooledObj != null)
        {
            pooledObj.spawnTime = Time.time;
        }
    }
}

// Thêm script này vào prefab của bạn
public class PooledObject : MonoBehaviour
{
    [HideInInspector] public float spawnTime;
}