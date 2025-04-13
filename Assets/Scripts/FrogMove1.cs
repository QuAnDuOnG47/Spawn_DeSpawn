using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    void OnEnable()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        speed = Random.Range(0.5f, 2f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
