using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float speedOfRotation = 0.15f;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        Vector3 velocity = new Vector3(moveDirection.x * _speed, _rigidbody.linearVelocity.y, moveDirection.z * _speed);
        _rigidbody.linearVelocity = velocity;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, speedOfRotation);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
    }
}
