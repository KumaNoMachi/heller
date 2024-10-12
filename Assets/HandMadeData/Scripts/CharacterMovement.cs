using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private int extraJumpsAllowed = 1;

    [Header("Editor")]
    [SerializeField] private bool IsDrawGizmoOn;
    [SerializeField] private float groundDistanceCheck = 2f;

    private KeyCode _attackKey = KeyCode.Mouse0, _jumpKey = KeyCode.Space;
    private bool _attamptAttack = false, _attemptJump = false;
    private float _xMoveIntention = 0f, _yMoveIntention = 0f;
    [SerializeField]private int _jumpIndex = 0;
    private Rigidbody2D _rB2D = null;

    private void Awake()
    {
        _rB2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    void Update()
    {
        GetInput();

        HandleJump();
        HandleAttack();
    }
    private void FixedUpdate()
    {
        HandleRun();
    }

    /// <summary>
    /// Returns false if Raycast found no colliders under the player
    /// </summary>
    /// <returns></returns>
    private bool CheckGrounded() { return Physics2D.Raycast(transform.position, -Vector2.up, groundDistanceCheck, groundLayer); }

    private void HandleAttack()
    {
        //tbc
    }
    private void HandleJump()
    {
        bool _isGrounded = CheckGrounded();
        if (_isGrounded) { _jumpIndex = 0; }

        if (_attemptJump && (_isGrounded || _jumpIndex < extraJumpsAllowed))
        {
            _rB2D.velocity = new Vector2(_rB2D.velocity.x, jumpForce);
            _jumpIndex++;
        }
    }
    /// <summary>
    /// Handles Movement in Fixed Update
    /// </summary>
    private void HandleRun()
    {
        //Based on right faced images
        if (_xMoveIntention < 0 && transform.rotation.y == 0)//if we wanna move left and not facing left
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (_xMoveIntention > 0 && transform.rotation.y != 0)//if we wanna move right and not facing right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        _rB2D.velocity = new Vector2(_xMoveIntention * speed, _rB2D.velocity.y);
    }
    /// <summary>
    /// Caching Player Input
    /// </summary>
    private void GetInput()
    {
        _xMoveIntention = Input.GetAxis("Horizontal");
        _yMoveIntention = Input.GetAxis("Vertical");

        _attamptAttack = Input.GetKeyDown(_attackKey);
        _attemptJump = Input.GetKeyDown(_jumpKey);
    }

    #region Editor
    private void OnDrawGizmosSelected()
    {
        if (IsDrawGizmoOn)
        {
            if (CheckGrounded())
            {
                Debug.DrawRay(transform.position, -Vector2.up * groundDistanceCheck, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, -Vector2.up * groundDistanceCheck, Color.red);
            }
        }
    }
    #endregion

}