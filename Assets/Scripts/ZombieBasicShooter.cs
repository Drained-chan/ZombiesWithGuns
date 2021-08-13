using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBasicShooter : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform target;
    [SerializeField] private float FireRate = 5f;
    [SerializeField] private float immunity = 0.5f;
    private ZombieMovement zombieMovement;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        timeLeft = FireRate;
        zombieMovement = GetComponent<ZombieMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timeLeft);
        if (zombieMovement.isChasing)
            transform.right = target.transform.position - transform.position;
        if ((timeLeft < 0) && (zombieMovement.isChasing))
        {
            timeLeft = FireRate;
            Shoot();
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
        

    }
    void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If zombie has shot in the the last second, be immune to bullets for a certain amount of time
        if ((collision.gameObject.tag == "Bullet") && (timeLeft < FireRate - immunity))
            Destroy(gameObject);
    }
}
