using System;
using System.Collections;
using System.Collections.Generic;
using Features.Station;
using UnityEngine;

public class DisableButtonOnStationEnter : MonoBehaviour
{
    private void OnEnable()
    {
        Station.StationOpened += DisableThis;
        Station.StationClosed += EnableThis;
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
