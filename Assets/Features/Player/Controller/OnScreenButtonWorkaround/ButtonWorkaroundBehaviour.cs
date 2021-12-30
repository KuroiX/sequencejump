using System;
using UnityEngine;
using Foundations.Actions;
using Foundations.Queue;
using Features.Player.Controller.CharacterInput;

namespace Features.Player.Controller.OnScreenButtonWorkaround
{
    public class ButtonWorkaroundBehaviour : MonoBehaviour, ICharacterInput
    {
        private ResettableQueue<ICharacterAction> _actionQueue;
        public ButtonWorkaround buttonWorkaround;

        private void Awake()
        {
            _actionQueue = GetComponent<QueueHolder>().Queue;
        }
    
        private void OnEnable()
        {
            InitializeInput();
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
            Horizontal = ((FloatEventArgs) args).Value;
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

        public float Horizontal { get; set; }
        public bool JumpPerformed { get; set; }
        public bool JumpBuffered => Time.unscaledTime - JumpTimeStamp < 0.1f;

        public bool JumpCanceled { get; set; }
        public float JumpTimeStamp { get; set; }
        public float JumpEndTimeStamp { get; private set; }
    
        public bool DashPerformed { get; set; }
    }
}

