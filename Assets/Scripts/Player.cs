using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    public float Speed = 5;
    public bool CanDie = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0).normalized;

        //Swap Sprite whether you're going left or right
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Make character move
        transform.Translate(moveDelta * Time.deltaTime * Speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (CanDie)
                Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}
