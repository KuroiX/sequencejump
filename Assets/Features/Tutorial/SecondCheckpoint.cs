using System;
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
        
        // [SerializeField] private GameObject queueUi;
        // [SerializeField] private GameObject actionButton;
        

        private ResettableQueue<ICharacterAction> _queue;

        private void Start()
        {
            _queue = FindObjectOfType<QueueHolder>().Queue;
            SetupFirstState();
        }

        #region Fifth state

        private void SetupFifthState()
        {
            fifthArrow.SetActive(true);
        }        

        #endregion
        
        #region Forth state
        
        private void SetupForthState()
        {
            forthArrow.SetActive(true);
            Station.StationOpened += OnWhatever;
        }
        
        private void OnWhatever(object sender, EventArgs e)
        {
            TearDownForthState();
            SetupThirdState();
        }

        private void TearDownForthState()
        {
            forthArrow.SetActive(false);
            Station.StationOpened -= OnWhatever;
        }
        
        #endregion

        #region Third state

        private void SetupThirdState()
        {
            thirdArrow.SetActive(true);
            _queue.ItemEnqueued += OnItemEnqueued;
            Station.StationClosed += OnClosed;
        }

        private int _count;

        private void OnItemEnqueued(object sender, EventArgs e)
        {
            _count++;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            if (_count == 3)
            {
                TearDownThirdState();
                SetupFifthState();
            }
            else
            {
                TearDownThirdState();
                SetupForthState();
            }
        }

        private void TearDownThirdState()
        {
            thirdArrow.SetActive(false);
            _queue.ItemEnqueued -= OnItemEnqueued;
            Station.StationClosed -= OnClosed;
        }

        #endregion

        #region Second state
        
        private void OnStationOpened(object sender, EventArgs args)
        {
            TearDownSecondState();
            SetupThirdState();
        }

        private void SetupSecondState()
        {
            secondArrow.SetActive(true);
            Station.StationOpened += OnStationOpened;
        }

        private void TearDownSecondState()
        {
            secondArrow.SetActive(false);
            Station.StationOpened -= OnStationOpened;
        }
        
        #endregion
        
        #region First state

        private void SetupFirstState()
        {
            firstArrow.SetActive(true);
            _queue.ItemDequeued += OnItemDequeued;
            Station.StationOpened += OnStationOpenedInFirstState;
        }

        private void OnStationOpenedInFirstState(object sender, EventArgs e)
        {
            TearDownFirstState();
            Station.StationClosed += OnStationClosedInNullState;
        }


        private void TearDownFirstState()
        {
            firstArrow.SetActive(false);
            _queue.ItemDequeued -= OnItemDequeued;
            Station.StationOpened -= OnStationOpenedInFirstState;
        }

        private void OnStationClosedInNullState(object sender, EventArgs e)
        {
            SetupFirstState();
            Station.StationClosed -= OnStationClosedInNullState;
        }

        private void OnItemDequeued(object sender, EventArgs args)
        {
            TearDownFirstState();
            SetupSecondState();
        }
        
        #endregion
    }
}