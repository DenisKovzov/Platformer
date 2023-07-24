using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu()]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Movement")]
        public float WalkSpeed = 1;
        public float RunSpeed = 2;
        [Header("Jump")]
        public float JumpHeight = 1f;
        public float Gravity = -1f;
        public LayerMask GroundMask;
        public float groundDetectionRadius = 0.01f;

        public Vector2 size;
    }

}