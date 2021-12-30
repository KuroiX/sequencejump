using Features.Actions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.StationLogic
{
    [System.Serializable]
    public struct StationSettings
    {
        public int maxAssignableActions;
        [SerializeField] public ActionCounter[] actionCounts;
    }
}