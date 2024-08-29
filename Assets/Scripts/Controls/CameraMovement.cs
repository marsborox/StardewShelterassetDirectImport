using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{

    private CameraControls cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    public Camera camera;

    [Header("Horizontal Translation")]
    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;
    [Header("Horizontal Translation")]
    [SerializeField]
    private float acceleration = 10f;
    [Header("Horizontal Translation")]
    [SerializeField]
    private float damping = 15f;
    [Header("Vertical Translation")]
    [SerializeField]
    private float stepSize = 2f;
    [Header("Vertical Translation")]
    [SerializeField]
    private float zoomDampening = 7.5f;
    [Header("Vertical Translation")]
    [SerializeField]
    private float minHeight = 5f;
    [Header("Vertical Translation")]
    [SerializeField]
    private float maxHeight = 50f;
    [Header("Vertical Translation")]
    [SerializeField]
    private float zoomSpeed = 2f;
    [Header("Edge Movement")]
    [SerializeField]
    [Range(0f, 0.1f)]
    private float edgeTolerance = 0.05f;

    [Header("Camera Zoom")]
    [SerializeField] float cameraScale;
    [SerializeField] int speedCoefficient = 2;
    //value set in various functions 
    //used to update the position of the camera base object.
    private Vector3 targetPosition;

    private float zoomHeight;

    //used to track and maintain velocity w/o a rigidbody
    private Vector3 horizontalVelocity;
    private Vector3 lastPosition;

    //tracks where the dragging action started
    Vector3 startDrag;

    float cameraSize;
    
    private void Awake()
    {
        cameraActions = new CameraControls();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;

        //mystuff
        //cameraSize = this.GetComponentInChildren<Camera>().orthographicSize;
    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.z;//changed y to z
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;

        movement = cameraActions.Camera.MoveCamera;
        // cameraActions.Camera.ZoomCamera.performed += ZoomCamera; //for original zoom
        
        

        cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        // cameraActions.Camera.ZoomCamera.performed -= ZoomCamera; //for original zoom
        cameraActions.Camera.Disable();
    }

    private void Start()
    {
        EnableCameraOnStart();
        cameraScale = 10f;
    }
    private void Update()
    {
        //inputs
        GetKeyboardMovement();
        AdjustMaxSpeedForZoom();


        //CheckMouseAtScreenEdge();//temp shutdown*************************
        DragCamera();

        //move base and camera objects
        //UpdateVelocity();
        UpdateBasePosition();
        UpdateCameraPosition();
        CameraZoomControl();
        
        //mine added
        
    }
    void EnableCameraOnStart()
    { 
        camera.enabled = true;
    }
    void CameraZoomControl()
    {
        if (camera)
        {
            //Debug.Log(camera.orthographicSize);
            camera.orthographicSize = cameraScale;
        }
    }
    private void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0f;
        lastPosition = this.transform.position;
    }
    void AdjustMaxSpeedForZoom()
    {
        maxSpeed = cameraScale * speedCoefficient;
    }
    private void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                    + movement.ReadValue<Vector2>().y * GetCameraUp();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
            targetPosition += inputValue;
    }

    private void DragCamera()
    {
        if (!Mouse.current.rightButton.isPressed)
            return;

        //create plane to raycast to
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance))
        {
            if (Mouse.current.rightButton.wasPressedThisFrame)
                startDrag = ray.GetPoint(distance);
            else
                targetPosition += startDrag - ray.GetPoint(distance);
        }
    }
   
    private void CheckMouseAtScreenEdge()
    {
        //mouse position is in pixels
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 moveDirection = Vector3.zero;

        //horizontal scrolling
        if (mousePosition.x < edgeTolerance * Screen.width)
            moveDirection += -GetCameraRight();
        else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
            moveDirection += GetCameraRight();

        //vertical scrolling
        if (mousePosition.y < edgeTolerance * Screen.height)
            moveDirection += -GetCameraUp();
        else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
            moveDirection += GetCameraUp();

        targetPosition += moveDirection;
    }
    private void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            //create a ramp up or acceleration
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            //create smooth slow down
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }
        //reset for next frame
        targetPosition = Vector3.zero;
    }

    //this does zoom could be done better with input system
    private void OnGUI()
    {
        cameraScale += Input.mouseScrollDelta.y*(-1);
        if (cameraScale < 5f)
        { cameraScale = 5; }
    }
    //whole method changed y to z vector2 to vector3

    //original, not reallyo ur usecase, might dissect later
    
    private void OriginalZoomCamera(InputAction.CallbackContext obj)
    {
        float inputValue = -obj.ReadValue<Vector2>().x / 100f; 

        if (Mathf.Abs(inputValue) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.z + inputValue * stepSize;
            

            if (zoomHeight < minHeight)
                zoomHeight = minHeight;
            else if (zoomHeight > maxHeight)
                zoomHeight = maxHeight;
        }
        //Debug.Log("WeScrolling"+inputValue);
        
    }
        
    private void UpdateCameraPosition()
    {
        //set zoom target
          Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        //add vector for forward/backward zoom
          zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.z) * Vector3.forward;

        /*cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);*/
        cameraTransform.LookAt(this.transform);
    }
    //gets the horizontal forward vector of the camera
    private Vector3 GetCameraUp()
        
    {
        Vector3 up = cameraTransform.up;
        up.x = 0f;
        return up;
    }

    //gets the horizontal right vector of the camera
    private Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0f;
        return right;
    }
}
