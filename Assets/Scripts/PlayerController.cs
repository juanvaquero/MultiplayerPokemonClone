using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private const string SPEED = "Speed";


    public float Speed = 5f; // Player movement speed
    private Vector2 _moveDirection; // Current movement direction of the player

    private Rigidbody2D _rb; // Player's Rigidbody2D component
    private Animator _animator; // Player's Animator component


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _moveDirection = Vector2.zero; // Set the initial movement direction to (0, 0)
    }

    void Update()
    {
        // Get horizontal and vertical input axis values
        float horizontalInput = Input.GetAxis(HORIZONTAL);
        float verticalInput = Input.GetAxis(VERTICAL);

        // Set the movement direction based on input axis values
        _moveDirection = new Vector2(horizontalInput, verticalInput);

        //Set the axis values into the animator for animete the player
        _animator.SetFloat(HORIZONTAL, _moveDirection.x);
        _animator.SetFloat(VERTICAL, _moveDirection.y);

        //For detect if the player is moving calculate te sqrMagnitude of the movement direction
        //Use sqrMagnitude instead of magnitude because it is more efficient
        _animator.SetFloat(SPEED, _moveDirection.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Move the player based on the current movement direction and speed
        _rb.MovePosition(_rb.position + _moveDirection * Speed * Time.deltaTime);

    }
}
