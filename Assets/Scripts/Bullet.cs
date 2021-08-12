using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 200f;
    Transform target;
    Vector3 moveDirection;
    public float survival_time = 5f;
    float time_left;
    Rigidbody2D bulletBody;
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        time_left = survival_time;
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(moveDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (time_left < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
