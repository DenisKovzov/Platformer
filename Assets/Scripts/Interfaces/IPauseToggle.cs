using System;

namespace Platformer
{
    public interface IPauseToggle
    {
        public event Action OnTogglePause;
    }
}