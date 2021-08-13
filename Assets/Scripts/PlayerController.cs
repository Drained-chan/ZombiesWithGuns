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
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //point towards mouse
        transform.rotation = MathUtils.Rotation.FromVector(
            MouseUtils.GetWorldMousePos(transform.position) - transform.position);

        //the velocity the player has signalled they want to go
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * maxSpeed;

        Vector2 netForce = acceleration * Time.deltaTime * (targetVelocity - rigidbody2d.velocity);

        rigidbody2d.AddForce(netForce);
    }
}
