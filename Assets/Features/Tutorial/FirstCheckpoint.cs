using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features.Tutorial
{
    public class FirstCheckpoint : MonoBehaviour
    {
        [SerializeField] private GameObject firstArrow;
        [SerializeField] private GameObject secondArrow;
        [SerializeField] private GameObject thirdArrow;
        [SerializeField] private GameObject forthArrow;

        [SerializeField] private GameObject queueUi;
        [SerializeField] private GameObject actionButton;

        [SerializeField] private SecondCheckpoint secondCheckpoint;
        
        private ResettableQueue<ICharacterAction> _queue;

        private void Awake()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            queueUi.SetActive(false);
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
                    firstArrow.SetActive(true);
                },
                () =>
                {
                    firstArrow.SetActive(false);
                });
            
            TutorialState secondState = new TutorialState(
                handler => Station.StationOpened += handler,
                handler => Station.StationOpened -= handler,
                handler => Station.StationExited += handler,
                handler => Station.StationExited -= handler,
                () => {
                    secondArrow.SetActive(true);
                    queueUi.SetActive(false);
                },
                () =>
                {
                    secondArrow.SetActive(false);
                });
            
            TutorialState thirdState = new TutorialState(
                handler => _queue.ItemEnqueued += handler,
                handler => _queue.ItemEnqueued -= handler,
                handler => Station.StationClosed += handler,
                handler => Station.StationClosed -= handler,
                () => {
                    thirdArrow.SetActive(true);
                    queueUi.SetActive(true);
                },
                () =>
                {
                    thirdArrow.SetActive(false);
                });
            
            TutorialState forthState = new TutorialState(
                handler => Station.StationClosed += handler,
                handler => Station.StationClosed -= handler,
                null, null,
                () => {
                    forthArrow.SetActive(true);
                },
                () =>
                {
                    forthArrow.SetActive(false);
                    actionButton.SetActive(true);
                    secondCheckpoint.enabled = true;
                    this.enabled = false;
                });

            TutorialState.Connect(firstState, secondState);
            TutorialState.Connect(secondState, thirdState);
            TutorialState.Connect(thirdState, forthState);
            firstState.Setup();
        }
    }
}