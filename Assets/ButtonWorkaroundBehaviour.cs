using System;
using System.Collections;
using System.Collections.Generic;
using Features.Actions;
using Features.Player;
using Features.Player.Controller;
using Features.Queue;
using Features.Station;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonWorkaroundBehaviour : MonoBehaviour, ICharacterInput
{
    private ResettableQueue<ICharacterAction> _actionQueue;
    private InputManager _inputManager;
    public ButtonWorkaround buttonWorkaround;

    private void DisableInput(object sender, EventArgs args)
    {
        _inputManager.PlayerMovement.Disable();
    }
        
    private void EnableInput(object sender, EventArgs args)
    {
        _inputManager.PlayerMovement.Enable();
    }
    
    private void Awake()
    {
        _actionQueue = GetComponent<QueueHolder>().Queue;
        
        _inputManager = new InputManager();
        _inputManager.PlayerMovement.Enable();

    }
    
    private void OnEnable()
    {
        InitializeInput();
            
        Station.StationOpened += DisableInput;
        Station.StationClosed += EnableInput;
            
        HazardTriggerEnter.DeathAnimationStart += DisableInput;
        HazardTriggerEnter.DeathAnimationEnd += EnableInput;
    }

    private void InitializeInput()
    {
        // TODO: "Action" input

        buttonWorkaround.LeftEvent += Move;
        buttonWorkaround.RightEvent += Move;
        buttonWorkaround.ActionDownEvent += ActionDown;
        buttonWorkaround.ActionUpEvent += JumpEnd;
    }

    private void OnDisable()
    {
        TerminateInput();
            
        Station.StationOpened -= DisableInput;
        Station.StationClosed -= EnableInput;
            
        HazardTriggerEnter.DeathAnimationStart -= DisableInput;
        HazardTriggerEnter.DeathAnimationEnd -= EnableInput;
    }

    private void TerminateInput()
    {
        // TODO: "Action" input

        buttonWorkaround.LeftEvent -= Move;
        buttonWorkaround.RightEvent -= Move;
        buttonWorkaround.ActionDownEvent -= ActionDown;
        buttonWorkaround.ActionUpEvent -= JumpEnd;

    }

    private void Move(object obj, EventArgs args)
    {
        Horizontal = ((WorkaroundEventArgs) args).Value;
    }

    private void ActionDown(object obj, EventArgs args)
    {
        // TODO: save currentAction in case of jump for short-hop
        if (_actionQueue.Count == 0) return;
        
        ICharacterAction currentAction = _actionQueue.Dequeue();

        switch (currentAction.Name)
        {
            case "Jump":
                Jump();
                break;
            case "Dash":
                Dash();
                break;
            default:
                throw new NotImplementedException(
                    "An Action was dequeued that has not been implemented in QueueProcessor.cs");
        }
    }

    private bool _lastActionWasJump;

    private void Jump()
    {
        Debug.Log("Jump");
        JumpPerformed = true;
        JumpTimeStamp = Time.unscaledTime;
        _lastActionWasJump = true;
    }

    private void JumpEnd(object obj, EventArgs args)
    {
        if (!_lastActionWasJump) return;
        JumpCanceled = true;
        JumpEndTimeStamp = Time.unscaledTime;
    }

    private void Dash()
    {
        Debug.Log("Dash bei mir");
        DashPerformed = true;
    }

    private void LateUpdate()
    {
        JumpPerformed = false;
        JumpCanceled = false;
        DashPerformed = false;
    }

    public float Horizontal { get; private set; }
    public bool JumpPerformed { get; private set; }
    public bool JumpBuffered => Time.unscaledTime - JumpTimeStamp < 0.1f;

    public bool JumpCanceled { get; private set; }
    public float JumpTimeStamp { get; set; }
    public float JumpEndTimeStamp { get; private set; }
    
    public bool DashPerformed { get; private set; }
}

