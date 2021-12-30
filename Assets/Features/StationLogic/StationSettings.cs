using UnityEngine;
using Foundations.Actions;

namespace Features.StationLogic
{
    [System.Serializable]
    public struct StationSettings
    {
        public int maxAssignableActions;
        [SerializeField] public ActionCounter[] actionCounts;
    }
}