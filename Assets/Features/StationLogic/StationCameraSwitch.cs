using System;
using Cinemachine;

namespace Features.StationLogic
{
    public class StationCameraSwitch
    {
        private readonly Station _station;

        private readonly CinemachineVirtualCamera _mainCamera;
        private readonly CinemachineVirtualCamera _stationCamera;

        public StationCameraSwitch(Station station, CinemachineVirtualCamera mainCamera, CinemachineVirtualCamera stationCamera)
        {
            _station = station;
            _mainCamera = mainCamera;
            _stationCamera = stationCamera;
        }

        public void HandleOnEnable()
        {
            Station.StationEntered += Subscribe;
            Station.StationExited += Unsubscribe;
        }

        public void HandleOnDisable()
        {
            Station.StationEntered -= Subscribe;
            Station.StationExited -= Unsubscribe;
        }

        private void Subscribe(object sender, EventArgs e)
        {
            if (_station != (sender)) return;
            //Debug.Log("Subscribe CameraSwitch");
            Station.StationOpened += CameraSwitchOnOpened;
            Station.StationClosed += CameraSwitchOnClosed;
        }

        private void Unsubscribe(object sender, EventArgs e)
        {
            if (_station != ((Station) sender)) return;
            //Debug.Log("Unsubscribe CameraSwitch");
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
            _stationCamera.Priority = _mainCamera.Priority + (enteredStation ? 1 : -1);
        }
        
    }
}
