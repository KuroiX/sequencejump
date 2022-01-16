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
            TutorialState zeroState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    Station.StationClosed += handler;
                }
                else
                {
                    Station.StationClosed -= handler;
                }
            },  null, 
                b =>
            {
                firstArrow.SetActive(!b);
            });
            
            TutorialState firstState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    _queue.ItemDequeued += handler;
                }
                else
                {
                    _queue.ItemDequeued -= handler;
                }
            }, (handler, b) =>
            {
                if (b)
                {
                    Station.StationOpened += handler;
                }
                else
                {
                    Station.StationOpened -= handler;
                }
            }, b =>
            {
                firstArrow.SetActive(b);
            });
            
            TutorialState secondState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    upperTrigger.TriggerEntered += handler;
                }
                else
                {
                    upperTrigger.TriggerEntered -= handler;
                }
            },  null, 
                b =>
            {
                secondArrow.SetActive(b);
            });
            
            TutorialState thirdState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    Station.StationOpened += handler;
                }
                else
                {
                    Station.StationOpened -= handler;
                }
            }, (handler, b) =>
            {
                if (b)
                {
                    lowerTrigger.TriggerEntered += handler;
                }
                else
                {
                    lowerTrigger.TriggerEntered -= handler;
                }
            }, b =>
            {
                forthArrow.SetActive(b);
            });

            CountEvent countEvent = new CountEvent(3, _queue);
            
            TutorialState forthState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    countEvent.ClosedAndFinished += handler;
                }
                else
                {
                    countEvent.ClosedAndFinished -= handler;
                }
            }, (handler, b) =>
            {
                if (b)
                {
                    countEvent.ClosedAndNotFinished += handler;
                }
                else
                {
                    countEvent.ClosedAndNotFinished -= handler;
                }
            }, b =>
            {
                thirdArrow.SetActive(b);
            });
            
            TutorialState fifthState = new TutorialState(null, null, b =>
            {
                fifthArrow.SetActive(true);
            });
            
            TutorialState.Connect(zeroState, firstState);
            TutorialState.Connect(firstState, secondState);
            TutorialState.Connect(secondState, thirdState);
            TutorialState.Connect(thirdState, forthState);
            TutorialState.Connect(forthState, fifthState);
            firstState.Setup();
        }
    }
}