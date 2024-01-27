using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;

    public InputAction Select;
    public InputAction SkipText;
    public InputAction ExitText;
    public InputAction Shear;
    public bool Pause;
    public bool IgnoreAllInputs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (inputManager.Paused())
        {
            Debug.Log("Paused");
            //Pause Stuff
            if(Pause)
            {
                Pause = false;
                IgnoreAllInputs = false;
            }
            else
            {
                Pause = true;
                IgnoreAllInputs = true;
            }
        }
        if (!IgnoreAllInputs)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);


            // Changes the height position of the player..
            //if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
            //{
            //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //}

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}