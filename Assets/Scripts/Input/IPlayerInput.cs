using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Platformer
{
    public interface IPlayerInput
    {
        public event Action OnJump;
        public float GetHorizontalMovement();
        public bool IsRunning();
    }
}