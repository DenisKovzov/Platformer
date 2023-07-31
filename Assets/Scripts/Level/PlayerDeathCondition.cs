using System;

namespace Platformer
{
    public class PlayerDeathCondition : ICondition
    {
        public event Action OnConditionMet;
        private Player player;
        private bool hasDeath;

        public PlayerDeathCondition(Player player)
        {
            this.player = player;

            player.OnDeath += Player_OnDeath;
        }

        private void Player_OnDeath()
        {
            hasDeath = true;
            OnConditionMet?.Invoke();
        }

        public bool IsConditionMet()
        {
            return hasDeath;
        }

        public void Reset()
        {
            hasDeath = false;
        }

        ~PlayerDeathCondition()
        {
            player.OnDeath -= Player_OnDeath;
        }
    }

}