using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Tutorial
{
    public class SecondCheckpoint : MonoBehaviour
    {
        [Header("Tutorial References")]
        [SerializeField] private GameObject actionButtonPointer;
        [SerializeField] private GameObject numberPointingArrow;
        [SerializeField] private GameObject stationHintingArrow;
        [SerializeField] private GameObject stationPointingArrow;
        [SerializeField] private GameObject levelPointingArrow;

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
                    actionButtonPointer.SetActive(false);
                },
                () =>
                {
                    actionButtonPointer.SetActive(true);
                });
            
            TutorialState firstState = new TutorialState(
                handler => _queue.ItemDequeued += handler,
                handler => _queue.ItemDequeued -= handler,
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                () => {
                    actionButtonPointer.SetActive(true);
                },
                () =>
                {
                    actionButtonPointer.SetActive(false);
                });
            
            TutorialState secondState = new TutorialState(
                handler => upperTrigger.TriggerEntered += handler,
                handler => upperTrigger.TriggerEntered -= handler,
                null, null,
                () => {
                    stationHintingArrow.SetActive(true);
                },
                () =>
                {
                    stationHintingArrow.SetActive(false);
                });
            
            TutorialState thirdState = new TutorialState(
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                handler => lowerTrigger.TriggerEntered += handler,
                handler => lowerTrigger.TriggerEntered -= handler,
                () => {
                    stationPointingArrow.SetActive(true);
                },
                () =>
                {
                    stationPointingArrow.SetActive(false);
                });

            CountEvent countEvent = new CountEvent(3, _queue);
            
            TutorialState forthState = new TutorialState(
                handler => countEvent.ClosedAndFinished += handler,
                handler => countEvent.ClosedAndFinished -= handler,
                handler => countEvent.ClosedAndNotFinished += handler,
                handler => countEvent.ClosedAndNotFinished -= handler,
                () => {
                    numberPointingArrow.SetActive(true);
                },
                () =>
                {
                    numberPointingArrow.SetActive(false);
                });
            
            TutorialState fifthState = new TutorialState(
                null,null, null, null,
                () => {
                    levelPointingArrow.SetActive(true);
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