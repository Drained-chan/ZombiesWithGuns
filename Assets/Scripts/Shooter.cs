using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject bullet;
    public Transform target;
    public float fire_rate = 5f;
    float time_left;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        time_left = fire_rate;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(time_left);
        transform.right = target.transform.position - transform.position;
        if (time_left < 0)
        {
            Shoot();
            time_left = fire_rate;
        }
        else
        {
            time_left -= Time.deltaTime;
        }
        

    }
void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
