using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;

    private Rigidbody2D _rb;

    //private CharacterController _cc;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_cc = GetComponent<CharacterController>();
        InitializeInput();
    }

    private void InitializeInput()
    {
        _inputManager = new InputManager();

        _inputManager.PlayerMovement.Enable();

        _inputManager.PlayerMovement.Jump.performed += Jump;
        _inputManager.PlayerMovement.Dash.performed += Dash;
        
        _inputManager.PlayerMovement.Move.performed += Move;
        _inputManager.PlayerMovement.Move.canceled += Move;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jump");
    }
    
    private void Dash(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dash");
    }
    
    private void Move(InputAction.CallbackContext ctx)
    {
        Debug.Log("Move");
    }
}
