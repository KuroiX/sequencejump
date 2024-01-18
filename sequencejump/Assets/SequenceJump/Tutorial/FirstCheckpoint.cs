using System.Collections.Generic;
using SequenceJump.Abilities;
using SequenceJump.Queue;
using SequenceJump.StationLogic;
using UnityEngine;

namespace SequenceJump.Tutorial
{
    public class FirstCheckpoint : MonoBehaviour
    {
        [Header("Tutorial References")]
        [SerializeField] private GameObject stationPointingArrow;
        [SerializeField] private GameObject openStationPointer;
        [SerializeField] private GameObject enqueueJumpPointer;
        [SerializeField] private GameObject closeStationPointer;
        
        [SerializeField] private SecondCheckpoint secondCheckpoint;
        
        [Header("Scene References")]
        [SerializeField] private GameObject queueUi;
        [SerializeField] private GameObject actionButton;

        private ResettableQueue<ICharacterAction> _queue;
        private Animator _queueAnimator;

        private readonly List<TutorialState> _states = new List<TutorialState>();

        private void Awake()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            queueUi.SetActive(false);
            _queueAnimator = queueUi.GetComponent<Animator>();
            actionButton.SetActive(false);
            
            HandleTutorialStates();
        }

        private void HandleTutorialStates()
        {
            TutorialState firstState = new TutorialState(
                handler => Station.StationEntered += handler,
                handler => Station.StationEntered -= handler,
                null, null,
                () =>
                {
                    stationPointingArrow.SetActive(true);
                },
                () =>
                {
                    stationPointingArrow.SetActive(false);
                });
            
            TutorialState secondState = new TutorialState(
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                handler => Station.StationExited += handler,
                handler => Station.StationExited -= handler,
                () => {
                    openStationPointer.SetActive(true);
                    queueUi.SetActive(false);
                },
                () =>
                {
                    openStationPointer.SetActive(false);
                });
            
            TutorialState thirdState = new TutorialState(
                handler => _queue.ItemEnqueued += handler,
                handler => _queue.ItemEnqueued -= handler,
                handler => Station.StationClosed += handler,
                handler => Station.StationClosed -= handler,
                () => {
                    enqueueJumpPointer.SetActive(true);
                    queueUi.SetActive(true);
                    _queueAnimator.SetTrigger("stationOpened");
                },
                () =>
                {
                    enqueueJumpPointer.SetActive(false);
                });
            
            TutorialState forthState = new TutorialState(
                handler => Station.StationClosed += handler,
                handler => Station.StationClosed -= handler,
                null, null,
                () => {
                    closeStationPointer.SetActive(true);
                },
                () =>
                {
                    closeStationPointer.SetActive(false);
                    actionButton.SetActive(true);
                    secondCheckpoint.enabled = true;
                    this.enabled = false;
                });

            TutorialState.Connect(firstState, secondState);
            TutorialState.Connect(secondState, thirdState);
            TutorialState.Connect(thirdState, forthState);
            firstState.Setup();
            
            _states.Add(firstState);
            _states.Add(secondState);
            _states.Add(thirdState);
            _states.Add(forthState);
        }

        private void OnDestroy()
        {
            foreach (var state in _states)
            {
                state.Teardown(true);
            }
        }
    }
}