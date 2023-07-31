using System;
using UnityEngine;

namespace Platformer
{
    public class DekstopPlayerInput : IPlayerInput
    {
        public event Action OnJump;
        public event Action OnTogglePause;

        public float GetHorizontalMovement()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public bool IsRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        public void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJump?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnTogglePause?.Invoke();
            }
        }
    }

}