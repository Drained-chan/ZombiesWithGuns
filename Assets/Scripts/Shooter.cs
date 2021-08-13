using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject bullet;
    public Transform target;
    public float FireRate = 5f;
    float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        timeLeft = FireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timeLeft);
        transform.right = target.transform.position - transform.position;
        if (timeLeft < 0)
        {
            Shoot();
            timeLeft = FireRate;
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
        if (collision.gameObject.tag == "Bullet") && (timeLeft < FireRate - 1)
            Destroy(gameObject);
    }
}
