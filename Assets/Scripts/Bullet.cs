using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 500f;
    Transform target;
    Vector3 moveDirection;
    public float SurvivalTime = 5f;
    float timeLeft;
    Rigidbody2D bulletBody;
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        moveDirection = (target.transform.position - transform.position).normalized * Speed;
        timeLeft = SurvivalTime;
        bulletBody = GetComponent<Rigidbody2D>();
        bulletBody.AddForce(moveDirection);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        

        if (timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
