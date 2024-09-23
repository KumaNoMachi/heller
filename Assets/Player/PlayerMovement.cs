using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1.5f;
    [SerializeField] private float jumpHeightMultiplier = 2f;

    public Rigidbody2D body;

    [SerializeField] private bool InAir;

    void Update()
    {
        if (InAir)
        {
            //Fill with air movement
        }
        else
        {
            //Caching the value from W,A,S,D buttons
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            //Creating a horizontal vector for movement
            Vector2 direction = new Vector2(xInput, 0);

            //Applying move direction
            body.velocity = direction * playerSpeed;

            //If there is vertical movement (Jump)
            if (yInput > 0)
            {
                InAir = true;
                direction = new Vector2(xInput, yInput);
                body.velocity = direction * playerSpeed * jumpHeightMultiplier;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!InAir) { return; } //checks if player is in the air to continue 

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))//if coliided object is equal to floor 
        {
            InAir = false;
        }
    }

}