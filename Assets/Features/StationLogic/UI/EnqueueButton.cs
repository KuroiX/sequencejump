using System;
using Features.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StationLogic.UI
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
            _button.onClick.AddListener(Enqueue);
            Station.StationChanged += SetText;
            _text.text = action.Name + " x" + Station.CurrentStation.ActionCounter.CurrentCount[action];
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Enqueue);
            Station.StationChanged -= SetText;
        }

        private void Enqueue()
        {
            Station.CurrentStation.EnqueueAction(action);
        }

        private void SetText(object sender, EventArgs args)
        {
            _text.text = action.Name + " x" + Station.CurrentStation.ActionCounter.CurrentCount[action];
        }
    }
}