using System;
using Features.StationLogic;
using UnityEngine;

namespace Features.Player
{
    public class DisableButtonOnStationEnter : MonoBehaviour
    {
        [SerializeField] private GameObject[] disableObjects;

        private void Awake()
        {
#if !UNITY_ANDROID
            gameObject.SetActive(false);
#endif
        }

        private void OnEnable()
        {
            Station.StationOpened += DisableChildren;
            Station.StationClosed += EnableChildren;
        }

        private void OnDisable()
        {
            Station.StationOpened -= DisableChildren;
            Station.StationClosed -= EnableChildren;
        }

        private void DisableChildren(object sender, EventArgs args)
        {
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].SetActive(false);
            }
        }
    
        private void EnableChildren(object sender, EventArgs args)
        {
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].SetActive(true);
            }
        }
    }
}
