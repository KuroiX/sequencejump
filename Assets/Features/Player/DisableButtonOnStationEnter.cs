using System;
using Features.StationLogic;
using UnityEngine;

namespace Features.Player
{
    public class DisableButtonOnStationEnter : MonoBehaviour
    {
        private void OnEnable()
        {
            Station.StationOpened += DisableThis;
            Station.StationClosed += EnableThis;
        }

        private void OnDisable()
        {
            Station.StationOpened -= DisableThis;
            Station.StationClosed -= EnableThis;
        }

        private void DisableThis(object sender, EventArgs args)
        {
            gameObject.SetActive(false);
        }
    
        private void EnableThis(object sender, EventArgs args)
        {
            gameObject.SetActive(true);
        }
    }
}
