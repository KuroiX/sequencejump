using System;
using Core.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace Features.StationLogic.UI
{
    public class EnqueueButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private bool isLeft;
        
        [SerializeField] private CharacterAction action;

        private void OnEnable()
        {
            button.onClick.AddListener(Enqueue);
            Station.StationChanged += SetText;
            SetText();
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Enqueue);
            Station.StationChanged -= SetText;
        }

        private void Enqueue()
        {
            Station.CurrentStation.EnqueueAction(action);
        }

        private void SetText(object sender, EventArgs args)
        {
            SetText();
        }

        private void SetText()
        {
            if (isLeft)
            {
                text.text = "x" + Station.CurrentStation.ActionCounter.CurrentCount[action];
            }
            else
            {
                text.text = Station.CurrentStation.ActionCounter.CurrentCount[action] + "x";
            }
            
        }
    }
}