using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerInputController : MonoBehaviour, IPlayerInput, IPauseable
    {

        public event Action OnJump;
        public event Action OnTogglePause;
        private IPlayerInput playerInput;
        private bool isPaused;

        public void Construct(IPlayerInput playerInput)
        {
            this.playerInput = playerInput;

            playerInput.OnJump += IPlayerInput_OnJump;
            playerInput.OnTogglePause += IPlayerInput_OnTogglePause;
        }

        private void IPlayerInput_OnTogglePause()
        {
            OnTogglePause?.Invoke();
        }

        private void IPlayerInput_OnJump()
        {
            if (!isPaused)
                OnJump?.Invoke();
        }

        public float GetHorizontalMovement()
        {
            return isPaused ? 0 : playerInput.GetHorizontalMovement();
        }

        public bool IsRunning()
        {
            return isPaused ? false : playerInput.IsRunning();
        }

        private void Update()
        {
            playerInput.UpdateInput();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }

        public void UpdateInput()
        {
        }
    }
}
