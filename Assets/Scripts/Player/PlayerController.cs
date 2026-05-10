using UnityEngine;
using Mirror;
using TASH.Core;

namespace TASH.Player
{
    /// <summary>
    /// Handles player movement and input
    /// </summary>
    public class PlayerController : NetworkBehaviour
    {
        private CharacterController characterController;
        private float moveSpeed = Constants.PLAYER_SPEED;
        private float sprintSpeed = Constants.PLAYER_SPRINT_SPEED;
        private Vector3 moveDirection;
        private float verticalVelocity = 0f;
        private float gravity = 9.81f;
        private Transform cameraHolder;
        private float xRotation = 0f;
        private float mouseSensitivity = 2f;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            
            if (!isLocalPlayer)
            {
                enabled = false;
                return;
            }
            
            cameraHolder = transform.Find("CameraHolder");
            if (cameraHolder == null)
            {
                GameObject camHolder = new GameObject("CameraHolder");
                camHolder.transform.SetParent(transform);
                camHolder.transform.localPosition = new Vector3(0, Constants.PLAYER_HEIGHT * 0.8f, 0);
                
                GameObject mainCam = new GameObject("MainCamera");
                mainCam.transform.SetParent(camHolder.transform);
                mainCam.transform.localPosition = Vector3.zero;
                mainCam.AddComponent<Camera>();
                mainCam.tag = "MainCamera";
                
                cameraHolder = camHolder.transform;
            }
        }

        private void Update()
        {
            if (!isLocalPlayer) return;

            HandleMovement();
            HandleCamera();
        }

        private void HandleMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = transform.right * horizontal + transform.forward * vertical;
            direction = direction.normalized;

            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

            moveDirection = direction * currentSpeed;

            // Gravity
            if (characterController.isGrounded)
            {
                verticalVelocity = -0.5f;
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }

            moveDirection.y = verticalVelocity;
            characterController.Move(moveDirection * Time.deltaTime);
        }

        private void HandleCamera()
        {
            if (cameraHolder == null) return;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraHolder.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}