using System;
using UnityEngine;

namespace Features.LevelEditor
{
    public class TestInstantiator : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Awake");
        }

        private void Start()
        {
            Debug.Log("Start");
        }

        private void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        public void Set()
        {
            Debug.Log("Set");
        }
    }
}