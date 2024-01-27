using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    [HideInInspector] public InputAction Pause;
    [HideInInspector] public InputAction SkipText;
    [HideInInspector] public InputAction Talk;
    [HideInInspector] public InputAction Shear;
    [HideInInspector] public InputAction Movement;
    [HideInInspector] public InputAction Jump;
    [HideInInspector] public InputAction Look;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private PlayerControls MainControls;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        MainControls = new PlayerControls();
        Cursor.visible = false;

        GetControls();
    }

    private void GetControls()
    {
        Pause = MainControls.Player.Pause;
        SkipText = MainControls.Player.SkipText;
        Talk = MainControls.Player.Talk;
        Shear = MainControls.Player.Shear;
        Movement = MainControls.Player.Movement;
        Jump = MainControls.Player.Jump;
        Look = MainControls.Player.Look;
    }

    private void OnEnable()
    {
        MainControls.Enable();
    }

    private void OnDisable()
    {
        MainControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseLook()
    {
        return Look.ReadValue<Vector2>();
    }
    public bool Paused()
    {
        return MainControls.Player.Pause.triggered;
    }

    public bool JumpStarted()
    {
        return Jump.triggered;
    }
}
