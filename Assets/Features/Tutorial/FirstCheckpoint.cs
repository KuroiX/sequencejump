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
                (handler, b) =>
            {
                if (b)
                {
                    Station.StationEntered += handler;
                } 
                else
                {
                    Station.StationEntered -= handler;
                }
            }, 
                null, 
                b =>
            {
                firstArrow.SetActive(b);
            });
            
            TutorialState secondState = new TutorialState((handler, b) =>
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
                    Station.StationExited += handler;
                }
                else
                {
                    Station.StationExited -= handler;
                }
            }, b =>
            {
                if (b)
                {
                    secondArrow.SetActive(true);
                    queueUi.SetActive(false);
                }
                else
                {
                    secondArrow.SetActive(false);
                }
            });
            
            TutorialState thirdState = new TutorialState((handler, b) =>
            {
                if (b)
                {
                    _queue.ItemEnqueued += handler;
                }
                else
                {
                    _queue.ItemEnqueued -= handler;
                }
            }, (handler, b) =>
            {
                if (b)
                {
                    Station.StationClosed += handler;
                }
                else
                {
                    Station.StationClosed -= handler;
                }
            }, b =>
            {
                if (b)
                {
                    thirdArrow.SetActive(true);
                    queueUi.SetActive(true);
                }
                else
                {
                    thirdArrow.SetActive(false);
                }
            });
            
            TutorialState forthState = new TutorialState((handler, b) =>
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
                if (b)
                {
                    forthArrow.SetActive(true);
                }
                else
                {
                    forthArrow.SetActive(false);
                    actionButton.SetActive(true);
                    secondCheckpoint.enabled = true;
                    this.enabled = false;
                }
            });
            
            TutorialState.Connect(firstState, secondState);
            TutorialState.Connect(secondState, thirdState);
            TutorialState.Connect(thirdState, forthState);
            firstState.Setup();
        }
        
        // #region Forth state
        //
        // private void SetupForthState()
        // {
        //     forthArrow.SetActive(true);
        //     Station.StationClosed += OnWhatever;
        // }
        //
        // private void OnWhatever(object sender, EventArgs e)
        // {
        //     TearDownForthState();
        //     secondCheckpoint.enabled = true;
        //     this.enabled = false;
        // }
        //
        // private void TearDownForthState()
        // {
        //     forthArrow.SetActive(false);
        //     actionButton.SetActive(true);
        //     Station.StationClosed -= OnWhatever;
        // }
        //
        // #endregion
        //
        // #region Third state
        //
        // private void SetupThirdState()
        // {
        //     thirdArrow.SetActive(true);
        //     queueUi.SetActive(true);
        //     _queue.ItemEnqueued += OnItemEnqueued;
        //     Station.StationClosed += OnClosed;
        // }
        //
        // private void OnItemEnqueued(object sender, EventArgs e)
        // {
        //     TearDownThirdState();
        //     SetupForthState();
        // }
        //
        // private void OnClosed(object sender, EventArgs e)
        // {
        //     TearDownThirdState();
        //     SetupSecondState();
        // }
        //
        // private void TearDownThirdState()
        // {
        //     thirdArrow.SetActive(false);
        //     _queue.ItemEnqueued -= OnItemEnqueued;
        //     Station.StationClosed -= OnClosed;
        // }
        //
        // #endregion
        //
        // #region Second state
        //
        // private void OnStationOpened(object sender, EventArgs args)
        // {
        //     TearDownSecondState();
        //     SetupThirdState();
        // }
        //
        // private void OnStationExited(object sender, EventArgs args)
        // {
        //     TearDownSecondState();
        //     SetupFirstState();
        // }
        //
        // private void SetupSecondState()
        // {
        //     secondArrow.SetActive(true);
        //     queueUi.SetActive(false);
        //     Station.StationExited += OnStationExited;
        //     Station.StationOpened += OnStationOpened;
        // }
        //
        // private void TearDownSecondState()
        // {
        //     secondArrow.SetActive(false);
        //     Station.StationExited -= OnStationExited;
        //     Station.StationOpened -= OnStationOpened;
        // }
        //
        // #endregion
        //
        // #region First state
        //
        // private void SetupFirstState()
        // {
        //     firstArrow.SetActive(true);
        //     Station.StationEntered += OnStationEntered;
        // }
        //
        // private void TearDownFirstState()
        // {
        //     firstArrow.SetActive(false);
        //     Station.StationEntered -= OnStationEntered;
        // }
        //
        // private void OnStationEntered(object sender, EventArgs args)
        // {
        //     TearDownFirstState();
        //     SetupSecondState();
        // }
        //
        // #endregion
    }
}