using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PauseState : State
    {
        private IPlayerInput input;
        private List<IPauseable> pausableList;
        private PauseMenuUI pauseMenu;

        public PauseState(
            GameStateMachine stateMachine,
            IPlayerInput input, 
            List<IPauseable> pausableList,
            PauseMenuUI pauseMenu) : base(stateMachine)
        {
            this.input = input;
            this.pausableList = pausableList;
            this.pauseMenu = pauseMenu;
        }

        public override void Enter()
        {
            input.OnTogglePause += IPlayerInput_OnTogglePause;

            foreach (var pausableObject in pausableList)
            {
                pausableObject?.Pause();
            }

            Time.timeScale = 0f;

            pauseMenu.Show();
        }

        public override void Exit()
        {
            input.OnTogglePause -= IPlayerInput_OnTogglePause;


            foreach (var pausableObject in pausableList)
            {
                pausableObject?.Resume();
            }

            Time.timeScale = 1f;
            
            pauseMenu.Hide();
        }


        private void IPlayerInput_OnTogglePause()
        {
            StateMachine.EnterIn<GameplayState>();
        }
    }

}