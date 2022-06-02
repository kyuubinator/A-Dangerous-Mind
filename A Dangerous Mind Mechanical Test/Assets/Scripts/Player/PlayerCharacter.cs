using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    #region References

    Rigidbody rigidBody;
    [Header("References")]
    [SerializeField] private Transform grabPosition;
    [SerializeField] private GameObject grabbedObject;
    [SerializeField] private ObjectToGrab grabbedObjectScript;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager ui;
    [SerializeField] private GameObject candleLight;

    [Header("Movement")]
    [SerializeField] private float maxMovementSpeed;
    [SerializeField] private float movementSpeed;

    [Header("Mouse")]
    private Vector3 mouseDelta;
    private float mouseSensitivity = 2;
    private bool cameraLock;

    [Header("Grab")]
    private bool isGrabbing;
    private float grabPositionCurrentX;
    private float grabPositionCurrentY;

    [Header("Checks and Restrains")]
    private bool outOfCD;
    private float switchCD;
    private bool freezeMovement;
    private bool hidden;

    [Header("Candle")]
    [SerializeField] private Animator animCandle;
    [SerializeField] private float candleAnimCooldown;
    [SerializeField] private float candleLifeTime;
    [SerializeField] private bool candleOutOfTime;
    private float candleAnimTimer;
    private bool candleBool;

    [Header("KeyItemsChecks")]
    [SerializeField] private bool KeyBedroom;
    [SerializeField] private bool KeyMasterBedroom;
    [SerializeField] private bool KeyBathroom;
    [SerializeField] private bool KeyLivingroom;
    [SerializeField] private bool KeyKitchen;
    [SerializeField] private bool KeyBox;
    [SerializeField] private int[] keysInInventory;
    //[SerializeField] private bool ;
    //[SerializeField] private bool ;

    public bool CameraLock { get => cameraLock; }
    public bool IsGrabbing { get => isGrabbing; set => isGrabbing = value; }
    public GameObject GrabbedObject { get => grabbedObject; set => grabbedObject = value; }
    public Transform GrabPosition { get => grabPosition; set => grabPosition = value; }
    public bool FreezeMovement { get => freezeMovement; set => freezeMovement = value; }
    public bool KeyMasterBedroom1 { get => KeyMasterBedroom; set => KeyMasterBedroom = value; }
    public bool KeyBathroom1 { get => KeyBathroom; set => KeyBathroom = value; }
    public bool KeyLivingroom1 { get => KeyLivingroom; set => KeyLivingroom = value; }
    public bool KeyKitchen1 { get => KeyKitchen; set => KeyKitchen = value; }
    public bool KeyBox1 { get => KeyBox; set => KeyBox = value; }
    public int[] KeysInInventory { get => keysInInventory; set => keysInInventory = value; }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        candleAnimTimer += Time.deltaTime;
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
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (candleAnimTimer >= candleAnimCooldown && !candleOutOfTime)
            {
                LightCandle();
                candleAnimTimer = 0;
            }
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
        if (candleBool)
        {
            candleLifeTime -= Time.deltaTime;
            if (candleLifeTime <= 0)
            {
                candleOutOfTime = true;
                if (!candleBool)
                {
                    LightCandle();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!freezeMovement && !ui.PauseActive && !ui.GameOver)
        MoveInDirection(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    #endregion

    #region Move

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

    #endregion

    #region Interact and rotate

    public void TryToInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            IGrabable grabable = hit.transform.GetComponent<IGrabable>();
            IReadable readable = hit.transform.GetComponent<IReadable>();
            IHideble hideble = hit.transform.GetComponent<IHideble>();
            IPickable pickup = hit.transform.GetComponent<IPickable>();
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
            if (pickup != null)
            {
                pickup.Pickup(this, ui);
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

    #endregion

    private void LightCandle()
    {
        candleBool = !candleBool;
        if (candleBool)
        {
            StartCoroutine(LitCandleAnimation());
        }
        else
        {
            StartCoroutine(UnlitCandleAnimation());
        }
    }

    IEnumerator LitCandleAnimation()
    {
        candleLight.SetActive(true);
        animCandle.SetBool("Light", true);
        yield return new WaitForSeconds(1f);

    }

    IEnumerator UnlitCandleAnimation()
    {
        animCandle.SetBool("Light", false);
        yield return new WaitForSeconds(1f);
        candleLight.SetActive(false);

    }

    public void AddLightCandle(float Value)
    {
        candleOutOfTime = false;
        candleLifeTime += Value;
    }

    public void EnableBools(int value)
    {
        switch (value)
        {
            case 1:
                KeyBedroom = true;
                keysInInventory[0] = 1;
                break;
            case 2:
                KeyMasterBedroom = true;
                keysInInventory[1] = 2;
                break;
            case 3:
                KeyBathroom = true;
                keysInInventory[2] = 3;
                break;
            case 4:
                KeyLivingroom = true;
                keysInInventory[3] = 4;
                break;
            case 5:
                KeyKitchen = true;
                keysInInventory[4] = 5;
                break;
        }
    }
}