using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour 
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions playerActions;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerActions.Jump.performed += ctx => playerMotor.Jump();
    }

    void FixedUpdate()
    {
        playerMotor.ProcessMove(playerActions.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(playerActions.Look.ReadValue<Vector2>());    
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
}
