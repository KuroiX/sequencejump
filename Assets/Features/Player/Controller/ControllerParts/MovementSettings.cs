using UnityEngine;

namespace Features.Player.Controller
{
    [System.Serializable]
    public class MovementSettings
    {
        [Header("Ground Settings")]
        public float groundSpeed;
        public float groundAcceleration;
        [Tooltip("Copy from ground options if no other behavior is wanted.")]
        [Header("Air Settings")]
        public float airSpeed;
        public float airAcceleration;
    }
}