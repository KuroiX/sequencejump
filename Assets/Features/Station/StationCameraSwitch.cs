using System;
using UnityEngine;

namespace Features.Station
{
    public class StationCameraSwitch : MonoBehaviour
    {
        private Camera _mainCamera;
        private Camera _stationCamera;
        private Station _station;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _stationCamera = GetComponentInChildren<Camera>();
        }

        private void Start()
        {
            _station = GetComponent<StationBehaviour>().Station;
        }

        private void OnEnable()
        {
            Station.StationEntered += Subscribe;
            Station.StationExited += Unsubscribe;
        }

        private void OnDisable()
        {
            Station.StationEntered -= Subscribe;
            Station.StationExited -= Unsubscribe;
        }

        private void Subscribe(object sender, EventArgs e)
        {
            if (_station != (sender)) return;
            Debug.Log("Subscribe CameraSwitch");
            Station.StationOpened += CameraSwitchOnOpened;
            Station.StationClosed += CameraSwitchOnClosed;
        }

        private void Unsubscribe(object sender, EventArgs e)
        {
            if (_station != ((Station) sender)) return;
            Debug.Log("Unsubscribe CameraSwitch");
            Station.StationOpened -= CameraSwitchOnOpened;
            Station.StationClosed -= CameraSwitchOnClosed;
        }

        private void CameraSwitchOnOpened(object sender, EventArgs e)
        {
            CameraSwitch(true);
        }
        private void CameraSwitchOnClosed(object sender, EventArgs e)
        {
            CameraSwitch(false);
        }

        private void CameraSwitch(bool enteredStation)
        {
            _mainCamera.enabled = !enteredStation;
            _stationCamera.enabled = enteredStation;
        }
        
    }
}
