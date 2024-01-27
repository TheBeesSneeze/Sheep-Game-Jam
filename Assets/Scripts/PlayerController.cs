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
    [SerializeField]
    private float sensitivity = 20;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    public bool Pause;
    public GameObject PauseScreen;
    public bool IgnoreAllInputs;

    private float xMovement;
    private float yMovement;

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
        cameraTransform = Camera.main.transform;

        InputManager.Instance.Pause.started += Pause_started;
    }

    void Update()
    {
        if (IgnoreAllInputs)
        {
            return;
        }

        /*
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        */

        yMovement += InputManager.Instance.GetMouseLook().y * sensitivity * Time.deltaTime;
        xMovement += InputManager.Instance.GetMouseLook().x * sensitivity * Time.deltaTime;
        yMovement = Mathf.Clamp(yMovement, -90, 90);
        Camera.main.transform.localEulerAngles = new Vector3(-yMovement, 0f, 0f);
        transform.eulerAngles = new Vector3(0f, xMovement, 0f);

        Vector3 movement = InputManager.Instance.GetPlayerMovement();//
        Vector3 move = transform.TransformDirection(movement.x, 0f, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        //move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        //move.y = 0f;


        // Changes the height position of the player..
        //if (inputManager.JumpStarted() && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Pause_started(InputAction.CallbackContext obj)
    {
        //Pause Stuff
        if (Pause)
        {
            PauseScreen.SetActive(false);
            Pause = false;
            Cursor.visible = false;
            IgnoreAllInputs = false;
        }
        else
        {
            PauseScreen.SetActive(true);
            Pause = true;
            Cursor.visible = true;
            IgnoreAllInputs = true;
        }
    }

    public void UnPause()
    {
        PauseScreen.SetActive(false);
        Pause = false;
        Cursor.visible = false;
        IgnoreAllInputs = false;
    }
}
