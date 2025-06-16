using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;        
    [SerializeField] private float distance;    
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float verticalMin = -30f;
    [SerializeField] private float verticalMax = 60f;
    [SerializeField] private float smoothSpeed = 10f;

    private float rotationX = 0f;  
    private float rotationY = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, verticalMin, verticalMax);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 desiredPosition = target.position - rotation * Vector3.forward * distance;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        transform.LookAt(target);
    }
}

