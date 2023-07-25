using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DekstopPlayerInput : MonoBehaviour, IPlayerInput
    {
        public event Action OnJump;

        public float GetHorizontalMovement()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public bool IsRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJump?.Invoke();
            }
        }
    }

}