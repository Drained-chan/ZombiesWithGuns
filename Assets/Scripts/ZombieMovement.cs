using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    Transform target;
    public float Speed = 4f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float wanderTime = 5f;
    private float wanderTimeCounter;
    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        wanderTimeCounter = wanderTime;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Once wander time expires, set a new waypoint and reset the timer
        wanderTimeCounter -= Time.deltaTime;
        if (wanderTimeCounter < 0)
        {
            wanderTimeCounter = wanderTime;
            var waypoint = Waypoint(range);
        }
        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);

        
    }


    // Waypoint returns two floats, so we can use them as coordinates.
    (float, float) Waypoint(float range) //Returns two floats for use as a waypoint
    {
        float x = Random.Range(-range, range);
        float y = Random.Range(-range, range);
        return (x, y);
    }
    void Wander(float x, float y)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}