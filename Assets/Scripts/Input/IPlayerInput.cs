using System;

namespace Platformer
{
    public interface IPlayerInput : IPauseToggle
    {
        public event Action OnJump;
        public float GetHorizontalMovement();
        public bool IsRunning();

        public void UpdateInput();
    }
}
