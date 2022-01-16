using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Tutorial
{
    public class SecondCheckpoint : MonoBehaviour
    {
        [SerializeField] private GameObject firstArrow;
        [SerializeField] private GameObject secondArrow;
        [SerializeField] private GameObject thirdArrow;
        [SerializeField] private GameObject forthArrow;
        [SerializeField] private GameObject fifthArrow;

        [SerializeField] private TutorialTriggerEntered upperTrigger;
        [SerializeField] private TutorialTriggerEntered lowerTrigger;

        private ResettableQueue<ICharacterAction> _queue;

        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            
            HandleTutorialStates();
        }

        private void HandleTutorialStates()
        {
            TutorialState zeroState = new TutorialState(
                handler => Station.StationClosed += handler,
                handler => Station.StationClosed -= handler,
                null, null,
                () =>
                {
                    firstArrow.SetActive(false);
                },
                () =>
                {
                    firstArrow.SetActive(true);
                });
            
            TutorialState firstState = new TutorialState(
                handler => _queue.ItemDequeued += handler,
                handler => _queue.ItemDequeued -= handler,
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                () => {
                    firstArrow.SetActive(true);
                },
                () =>
                {
                    firstArrow.SetActive(false);
                });
            
            TutorialState secondState = new TutorialState(
                handler => upperTrigger.TriggerEntered += handler,
                handler => upperTrigger.TriggerEntered -= handler,
                null, null,
                () => {
                    secondArrow.SetActive(true);
                },
                () =>
                {
                    secondArrow.SetActive(false);
                });
            
            TutorialState thirdState = new TutorialState(
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                handler => lowerTrigger.TriggerEntered += handler,
                handler => lowerTrigger.TriggerEntered -= handler,
                () => {
                    forthArrow.SetActive(true);
                },
                () =>
                {
                    forthArrow.SetActive(false);
                });

            CountEvent countEvent = new CountEvent(3, _queue);
            
            TutorialState forthState = new TutorialState(
                handler => countEvent.ClosedAndFinished += handler,
                handler => countEvent.ClosedAndFinished -= handler,
                handler => countEvent.ClosedAndNotFinished += handler,
                handler => countEvent.ClosedAndNotFinished -= handler,
                () => {
                    thirdArrow.SetActive(true);
                },
                () =>
                {
                    thirdArrow.SetActive(false);
                });
            
            TutorialState fifthState = new TutorialState(
                null,null, null, null,
                () => {
                    fifthArrow.SetActive(true);
                }, null);
            
            TutorialState.Connect(zeroState, firstState);
            TutorialState.Connect(firstState, secondState);
            TutorialState.Connect(secondState, thirdState);
            TutorialState.Connect(thirdState, forthState);
            TutorialState.Connect(forthState, fifthState);
            
            firstState.Setup();
        }
    }
}