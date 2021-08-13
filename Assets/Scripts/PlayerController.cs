using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //the rate that the player approaches the max speed
    [SerializeField] float acceleration = 400.0f;
    //the maximum speed in any direction the player can move
    [SerializeField] float maxSpeed = 8.0f;
    //the rate the player decelerates to a stop
    [SerializeField] float deceleration = 400.0f;

    //cached rigidbody
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private Vector2 GetInputVector()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Update()
    {
        //point towards mouse
        transform.rotation = MathUtils.Rotation.FromVector(
            MouseUtils.GetWorldMousePos(transform.position) - transform.position);

        //the velocity the player has signalled they want to go
        Vector2 input = GetInputVector();
        Vector2 targetVelocity = input * maxSpeed;

        Vector2 netForce = acceleration * Time.deltaTime * (targetVelocity - rigidbody2d.velocity);

        rigidbody2d.AddForce(netForce);

        if(input.magnitude == 0)
        {
            rigidbody2d.AddForce(-rigidbody2d.velocity * Time.deltaTime * deceleration);


            if (rigidbody2d.velocity.magnitude < 0.5f)
            {
                rigidbody2d.velocity = Vector2.zero;
            }
        }
    }
}
