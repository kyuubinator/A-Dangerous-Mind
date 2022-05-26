using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    Rigidbody rigidBody;
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private GameObject grabbedObject;
    [SerializeField] private ObjectToGrab grabbedObjectScript;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager ui;
    private bool isGrabbing;
    private Vector3 mouseDelta;
    private float grabPositionCurrentX;
    private float grabPositionCurrentY;
    private float mouseSensitivity = 2;
    private bool cameraLock;
    private bool outOfCD;
    private float switchCD;
    private bool freezeMovement;
    private bool hidden;
    

    public bool CameraLock { get => cameraLock; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }
    public GameObject GrabbedObject { get => grabbedObject; set => grabbedObject = value; }
    public Transform GrabPosition { get => grabPosition; set => grabPosition = value; }
    public bool FreezeMovement { get => freezeMovement; set => freezeMovement = value; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToInteract();
        }
        if (Input.GetKey(KeyCode.R))
        {
            TryToRotate();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            cameraLock = false;;
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            if (outOfCD)
                inventory?.CicleItems();
        }
        if (!outOfCD)
        {
            switchCD += Time.deltaTime;
        }
        if (switchCD > 0.3f)
        {
            outOfCD = true;
        }
    }

    private void FixedUpdate()
    {
        if(!freezeMovement || ui.PauseActive)
        MoveInDirection(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void MoveInDirection(Vector2 direction)
    {
        Vector3 finalVelocity = (direction.x * transform.right + direction.y * transform.forward).normalized * movementSpeed;
        finalVelocity.y = rigidBody.velocity.y;
        rigidBody.velocity = finalVelocity;

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            movementSpeed += Time.deltaTime * 7;
            movementSpeed = Mathf.Clamp(movementSpeed, 0, maxMovementSpeed);
        }
        else
        {
            movementSpeed = 0;
        }
    }
    public void TryToInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            IGrabable grabable = hit.transform.GetComponent<IGrabable>();
            IReadable readable = hit.transform.GetComponent<IReadable>();
            IHideble hideble = hit.transform.GetComponent<IHideble>();
            if (interactable != null)
            {
                Debug.Log("Player Detected");
                interactable.Interact(this);
            }
            if (grabable != null)
            {
                if (!isGrabbing)
                {
                    grabable.Grab(grabPosition);
                    grabPosition.rotation = new Quaternion(0, 90, 0, 0);
                    grabbedObject = hit.transform.gameObject;
                    isGrabbing = true;
                    inventory.AddItem(hit.transform.gameObject);                    // Null Reference ???
                }
                else
                {
                    if (hit.transform.gameObject == grabbedObject)
                    {
                        inventory.RemoveItem(grabbedObject);                        // Null Reference ???
                        grabable.Grab(null);
                        grabbedObject = null;
                    }
                    if (grabbedObject == null)
                    {
                        isGrabbing = false;
                    }
                }

            }
            if (readable != null)
            {
                readable.Read();
                freezeMovement = !freezeMovement;
                rigidBody.velocity = Vector3.zero;
            }
            if (hideble != null)
            {
                if (!hidden)
                    hideble.Hide(this.gameObject);
            }
        }
    }

    private void TryToRotate()
    {
        if (isGrabbing)
        {
            cameraLock = true;

            mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

            grabPositionCurrentX -= mouseDelta.y;

            grabPositionCurrentY -= mouseDelta.x;

            grabPosition.transform.Rotate(Vector3.up * mouseDelta.x);

            grabPosition.transform.Rotate(Vector3.right * mouseDelta.y);
        }
    }
}