using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //the rate that the player approaches the max speed
    [SerializeField] float acceleration = 10.0f;
    //the maximum speed in any direction the player can move
    [SerializeField] float maxSpeed = 15.0f;

    //cached rigidbody
    Rigidbody2D rigidbody;

    private void Update()
    {

    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
