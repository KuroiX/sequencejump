using System;
using System.Collections;
using System.Collections.Generic;
using Features.Player.Controller;
using Features.Player.Controller.ControllerParts;
using Features.Player.DeathLogic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private string test;
    
    private Animator _animator;
    private GroundedController _groundedController;
    private IDashController _dashController;
    private DeathTriggerBehaviour _deathTriggerBehaviour;
    private MovementController _movementController;
    private JumpController _jumpController;
    private JumpController _airJumpController;
    

    private float prevX;

    private void Start()
    {
        CharControllerBehaviour charControllerBehaviour = GetComponentInParent<CharControllerBehaviour>();
        _animator = GetComponent<Animator>();
        _groundedController = charControllerBehaviour.Grounded;
        _dashController = charControllerBehaviour.Dash;
        _jumpController = charControllerBehaviour.Jump;
        _airJumpController = charControllerBehaviour.AirJump;
        _deathTriggerBehaviour = GetComponentInParent<DeathTriggerBehaviour>();
        prevX = transform.parent.position.x;
        
        
        _deathTriggerBehaviour.Stop += StartDeathAnimation;
        _deathTriggerBehaviour.Respawn += StartSpawnAnimation;
    }
    

    private void OnDisable()
    {
        _deathTriggerBehaviour.Stop -= StartDeathAnimation;
        _deathTriggerBehaviour.Respawn -= StartSpawnAnimation;
    }


    private void Update()
    {
        double var1 = Math.Round(prevX, 3);
        double var2 = Math.Round(transform.parent.localPosition.x, 3);
        
        
        //Debug.Log("Prev: "+var1+", Current: "+var2);

        
        if (!_groundedController.IsGrounded)
        {
            _animator.SetBool("IsAirborn", true);
        }
        else
        {
            _animator.SetBool("IsAirborn", false);
        }

        if (_dashController.IsDashing)
        {
            _animator.SetBool("IsDashing", true);
        }
        else
        {
            _animator.SetBool("IsDashing", false);
        }

        if (Math.Abs(var1 - var2) > 0.01f)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        if (_airJumpController.IsJumping)
        {
            _animator.SetTrigger("AirJump");
        }


        prevX = transform.parent.localPosition.x;
    }

    private void StartDeathAnimation(object obj, EventArgs evt)
    {
        _animator.SetTrigger("Death");
    }

    private void StartSpawnAnimation()
    {
        _animator.SetTrigger("Spawn");
    }
}
