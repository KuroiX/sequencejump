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
            Station.StationChanged += SetText;
            _text.text = action.Name + " x" + Station.CurrentStation.ActionCounter.CurrentAvailableActions[action];
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
            Station.StationChanged -= SetText;
        }

        private void SetText(object sender, EventArgs args)
        {
            _text.text = action.Name + " x" + ((StationEventArgs) args).Station.ActionCounter.CurrentAvailableActions[action];
        }
    }
}