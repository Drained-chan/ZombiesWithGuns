using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    Transform target;
    public float Speed = 4f;
    [SerializeField] private float range = 1f;
    [SerializeField] private float wanderTime = 5f;
    [SerializeField] private float chaseDistance = 3f;
    [SerializeField] private float chaseThreshold = 6f;
    private float wanderTimeCounter;
    public bool isChasing = false;
    private float xWaypoint = 0f;
    private float yWaypoint = 0f;

   

    // Start is called before the first frame update
    void Start()
    {
        wanderTimeCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move(GetComponent<Pathfinding>().path[0].worldPosition);
    }


    // Moves Zombie towards an (x, y) coordinate
    void Move(Vector2 waypoint)
    {
        Debug.Log("waypoint: "+ waypoint);
        transform.position = Vector2.MoveTowards(transform.position, waypoint, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If bullet collides with zombie, destroy bullet and destroy zombie
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}