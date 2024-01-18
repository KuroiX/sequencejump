using System;
using SequenceJump.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace SequenceJump.StationLogic.UI
{
    public class EnqueueButton : MonoBehaviour
    {
        [SerializeField] private GameObject buttonHolder;
        
        [SerializeField] private Button button;
        [SerializeField] private Text text;
        [SerializeField] private bool isLeft;
        
        [SerializeField] private CharacterAction action;

        private void OnEnable()
        {
            int count = Station.CurrentStation.AvailableCount[action];
            if (count > 0)
            {
                button.onClick.AddListener(Enqueue);
                Station.StationChanged += SetText;
                SetText();
                buttonHolder.SetActive(true);
            }
            else
            {
                buttonHolder.SetActive(false);
            }
        }

        private void OnDisable()
        {
            int count = Station.CurrentStation.AvailableCount[action];
            if (count > 0)
            {
                button.onClick.RemoveListener(Enqueue);
                Station.StationChanged -= SetText;
            }
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
                text.text = "x" + Station.CurrentStation.CurrentCount[action];
            }
            else
            {
                text.text = Station.CurrentStation.CurrentCount[action] + "x";
            }
            
        }
    }
}