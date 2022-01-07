using System;
using Core.Actions;
using Core.Queue;
using UnityEngine;
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

        private ICharacterInput[] _characterInput;

        private QueueInput _queueInput;

        [SerializeField] private bool useButtons = false;

        private void Awake()
        {
            _actionQueue = GetComponent<QueueHolder>().Queue;
            
            ICharacterInput buttonInput = FindObjectOfType<ButtonWorkaroundInput>();

            bool foundButtonInput = !ReferenceEquals(buttonInput, null);

            _characterInput = new ICharacterInput[foundButtonInput ? 2 : 1];

            _characterInput[0] = new InputManagerInput(new InputManager());

            if (foundButtonInput)
            {
                _characterInput[1] = buttonInput;
            }

            foreach (var input in _characterInput)
            {
                input.Enable();
            }

            QueueProcessor processor = new QueueProcessor(_actionQueue);

            _queueInput = new QueueInput(processor, _characterInput, this);
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
            foreach (var input in _characterInput)
            {
                input.Enable();
            }
        }
        
        private void EnableInput(object sender, EventArgs args)
        {
            foreach (var input in _characterInput)
            {
                input.Enable();
            }
        }
    }
}
