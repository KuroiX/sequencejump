using System;
using Features.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Station.UI
{
    public class EnqueueButton : MonoBehaviour
    {
        [SerializeField]
        private CharacterAction action;

        private Button _button;
        private Text _text;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<Text>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(() => Station.CurrentStation.EnqueueAction(action));
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => Station.CurrentStation.EnqueueAction(action));
        }
    }
}