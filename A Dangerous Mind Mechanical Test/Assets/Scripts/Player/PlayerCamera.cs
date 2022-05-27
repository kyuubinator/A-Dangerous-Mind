#region Namespaces/Directives

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class PlayerCamera : MonoBehaviour
{
    #region Declarations

    [Header("Camera Movement Settings")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float cameraCurrentX = 0;
    Vector2 mouseDelta;

    [SerializeField] private UIManager ui;
    [SerializeField] private PlayerCharacter player;


    #endregion

    #region MonoBehaviour
    private void LateUpdate()
    {
        if (player.CameraLock || player.FreezeMovement || ui.PauseActive || ui.GameOver)
            return;
        else
            UpdateMouseLook();
    }

    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateMouseLook()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseSensitivity;

        cameraCurrentX -= mouseDelta.y;

        cameraCurrentX = Mathf.Clamp(cameraCurrentX, -90, 90);

        Camera.main.transform.localEulerAngles = Vector3.right * cameraCurrentX;
        transform.Rotate(Vector3.up * mouseDelta.x);
    }
}

