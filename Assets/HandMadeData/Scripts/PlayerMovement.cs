using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;

    [SerializeField] private float _playerSpeed = 8.5f;
    [SerializeField] private int _maxJumpCount = 3;

    private float _horizontalMove = 0f, _verticalMove = 0f;
    private bool _onGround = false, _jumpPressed = false;
    private int _currentJumpIndex = 0;
    private bool _outOfJumps = false;

    void Update()
    {
        //Caching the value from Movement buttons
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");

        //Caching Jump button status
        if (Input.GetButtonDown("Jump")) { _jumpPressed = true; }
    }

    private void FixedUpdate()
    {
        if (_jumpPressed && !_outOfJumps)
        {
            _onGround = false;
            _currentJumpIndex++;
            if (_currentJumpIndex >= _maxJumpCount) { _outOfJumps = true; }
            //Creating a movement vector
            Vector2 direction = new Vector2(_horizontalMove, _verticalMove);
            //Applying move direction
            body.velocity = direction * _playerSpeed * Time.fixedDeltaTime * 100;

            //Fill with air movement
        }

        if (_onGround)
        {
            //Creating a horizontal vector for movement
            Vector2 direction = new Vector2(_horizontalMove, 0);
            //Applying move direction
            body.velocity = direction * _playerSpeed * Time.fixedDeltaTime;
        }

        _jumpPressed = false;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))//if coliided object is equal to floor 
        {
            _onGround = true;
            _outOfJumps = false;
            _currentJumpIndex = 0;
        }
    }

}