using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private Text fpsCounter;
        [SerializeField] private Text device;

        private float _time;
        private int _counter;
        
        private void Start()
        {
            device.text = "" + Screen.currentResolution.refreshRate;
            //Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }

        private void Update()
        {
            _time += Time.unscaledDeltaTime;
            _counter++;
            if (_time > 1)
            {
                fpsCounter.text = _counter.ToString();
                _time = 0;
                _counter = 0;
            }
        }
    }
}