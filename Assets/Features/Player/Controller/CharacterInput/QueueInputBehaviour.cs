using System;
using UnityEngine;
using Foundations.Queue;
using Foundations.Actions;
using Features.Player.DeathLogic;
using Features.StationLogic;

namespace Features.Player.Controller.CharacterInput
{
    public class QueueInputBehaviour : MonoBehaviour, IControllerInput
    {
        public float Horizontal { get; set; }
        public bool JumpPerformed { get; set; }
        public bool JumpBuffered => Time.unscaledTime - JumpTimeStamp < 0.1f;

        public bool JumpCanceled { get; set; }
        public float JumpTimeStamp { get; set; }
        public float JumpEndTimeStamp { get; private set; }
        
        public bool DashPerformed { get; set; }
        
        private ResettableQueue<ICharacterAction> _actionQueue;
        
        private InputManager _inputManager;

        private QueueInput _queueInput;
        
        private void Awake()
        {
            _actionQueue = GetComponent<QueueHolder>().Queue;
            
            _inputManager = new InputManager();
            _inputManager.PlayerMovement.Enable();

            QueueProcessor processor = new QueueProcessor(_actionQueue);

            _queueInput = new QueueInput(processor, new InputManagerInput(_inputManager), this);
        }
        
        private void OnEnable()
        {
            _queueInput.HandleOnEnable();
            
            Station.StationOpened += DisableInput;
            Station.StationClosed += EnableInput;
            
            DeathLogicBehaviour.DeathAnimationStart += DisableInput;
            DeathLogicBehaviour.DeathAnimationEnd += EnableInput;
        }

        private void OnDisable()
        {
            _queueInput.HandleOnDisable();
            
            Station.StationOpened -= DisableInput;
            Station.StationClosed -= EnableInput;
            
            DeathLogicBehaviour.DeathAnimationStart -= DisableInput;
            DeathLogicBehaviour.DeathAnimationEnd -= EnableInput;
        }

        private void LateUpdate()
        {
            _queueInput.HandleLateUpdate();
        }

        private void DisableInput(object sender, EventArgs args)
        {
            _inputManager.PlayerMovement.Disable();
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            _inputManager.PlayerMovement.Enable();
        }
    }
}
