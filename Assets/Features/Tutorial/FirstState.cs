using System;
using Features.StationLogic;
using UnityEngine;

namespace Features.Tutorial
{
    public class FirstState : TutorialState
    {
        public FirstState(GameObject[] gameObjects, TutorialState nextState) : base(gameObjects)
        {
            
        }
        
        public override void Setup()
        {
            base.Setup();
            Station.StationEntered += OnStationEntered;
        }
        
        private void OnStationEntered(object sender, EventArgs args)
        {
            TearDown();
            //SetupSecondState();
        }
        
        public override void TearDown()
        {
            base.TearDown();
            Station.StationEntered -= OnStationEntered;
        }
    }
}