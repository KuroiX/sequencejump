using Core.Actions;
using UnityEngine;

namespace Features.StationLogic
{
    [System.Serializable]
    public struct StationSettings
    {
        public int maxAssignableActions;
        [SerializeField] public ActionCounter[] actionCounts;
    }
}