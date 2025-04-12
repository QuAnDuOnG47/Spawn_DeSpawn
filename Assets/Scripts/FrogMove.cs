using UnityEngine;

public class FrogMove : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        direction = Random.insideUnitSphere;
        speed = Random.Range(1f, 5f);
        
    }
    void Start()
    {
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
         transform.Translate(direction * speed * Time.deltaTime);

    }
   
}
