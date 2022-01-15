using System;
using Core.Actions;
using Core.Queue;
using Features.StationLogic;
using UnityEngine;

namespace Features
{
    public class FirstCheckpoint : MonoBehaviour
    {
        [SerializeField] private GameObject firstArrow;
        [SerializeField] private GameObject secondArrow;
        [SerializeField] private GameObject thirdArrow;
        [SerializeField] private GameObject forthArrow;

        private ResettableQueue<ICharacterAction> _queue;

        private void Awake()
        {
            SetupFirstState();
            _queue = FindObjectOfType<QueueHolder>().Queue;
        }
        
        #region Forth state

        private void SetupForthState()
        {
            forthArrow.SetActive(true);
            Station.StationClosed += OnWhatever;
            _queue.QueueCleared += OnReset;
        }

        private void OnWhatever(object sender, EventArgs e)
        {
            TearDownForthState();
            //SetupForthState();
        }

        private void OnReset(object sender, EventArgs e)
        {
            Debug.Log("reset");
            TearDownForthState();
            SetupThirdState();
        }

        private void TearDownForthState()
        {
            forthArrow.SetActive(false);
            Station.StationClosed -= OnWhatever;
            _queue.QueueCleared -= OnReset;
        }

        #endregion

        #region Third state

        private void SetupThirdState()
        {
            thirdArrow.SetActive(true);
            _queue.ItemEnqueued += OnItemEnqueued;
            Station.StationClosed += OnClosed;
        }

        private void OnItemEnqueued(object sender, EventArgs e)
        {
            TearDownThirdState();
            SetupForthState();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            TearDownThirdState();
            SetupSecondState();
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

        private void OnStationExited(object sender, EventArgs args)
        {
            TearDownSecondState();
            SetupFirstState();
        }

        private void SetupSecondState()
        {
            secondArrow.SetActive(true);
            Station.StationExited += OnStationExited;
            Station.StationOpened += OnStationOpened;
        }

        private void TearDownSecondState()
        {
            secondArrow.SetActive(false);
            Station.StationExited -= OnStationExited;
            Station.StationOpened -= OnStationOpened;
        }
        
        #endregion
        
        #region First state

        private void SetupFirstState()
        {
            firstArrow.SetActive(true);
            Station.StationEntered += OnStationEntered;
        }

        private void TearDownFirstState()
        {
            firstArrow.SetActive(false);
            Station.StationEntered -= OnStationEntered;
        }
        
        private void OnStationEntered(object sender, EventArgs args)
        {
            TearDownFirstState();
            SetupSecondState();
        }
        
        #endregion
    }
}