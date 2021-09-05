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
    private List<Node> path;

   

    // Start is called before the first frame update
    void Start()
    {
        wanderTimeCounter = 0f;
        target = GameObject.Find("Player").GetComponent<Transform>();
        path = GetComponent<Pathfinding>().path;
    }

    // Update is called once per frame
    void Update()
    {
        //// Once wander time expires, set a new waypoint and reset the timer
        //wanderTimeCounter -= Time.deltaTime;
        //if (wanderTimeCounter < 0)
        //{
        //    wanderTimeCounter = wanderTime;
        //    xWaypoint = transform.position.x - Waypoint(range);
        //    yWaypoint = transform.position.y - Waypoint(range);
        //}
        //float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        //// If target passes distance threshold, switch isChasing
        //if (distance < chaseDistance)
        //    isChasing = true;
        //else if (distance > chaseThreshold)
        //    isChasing = false;

        //// If isChasing, move towards the player, else move to waypoint
        //if (isChasing)
        //{
        //    Move(target.position.x, target.position.y);
        //    transform.right = target.transform.position - transform.position;
        //}
        //else
        //{
        //    Move(xWaypoint, yWaypoint);
        //    if ((new Vector3(xWaypoint, yWaypoint, 0) - transform.position).magnitude > 0.1)
        //        transform.right = new Vector3(xWaypoint, yWaypoint, 0) - transform.position;
        //}
        Move(path[0].worldPosition);
        path = GetComponent<Pathfinding>().path;
    }


    // Waypoint returns two floats, so we can use them as coordinates.
    float Waypoint(float range) //Returns two floats for use as a waypoint
    {
        float x = Random.Range(-range, range);
        return x;
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
        // If collide with a wall, set the way point to your own position
        else if (collision.gameObject.tag == "Wall")
        {
            xWaypoint = gameObject.transform.position.x;
            yWaypoint = gameObject.transform.position.y;
        }
        
    }
}