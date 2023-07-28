using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EndGameState : State
    {
        private WonScreenUI wonScreenUI;
        private LoseScreenUI loseScreenUI;

        public EndGameState(
            GameStateMachine stateMachine, 
            WonScreenUI wonScreenUI, 
            LoseScreenUI loseScreenUI) : base(stateMachine)
        {
            this.wonScreenUI = wonScreenUI;
            this.loseScreenUI = loseScreenUI;
        }

        private bool hasWin = false;

        public override void Enter()
        {
            if (hasWin)
            {
                wonScreenUI.Show();
            }
            else
            {
                loseScreenUI.Show();
            }
        }

        public override void Exit()
        {
            wonScreenUI.Hide();
            loseScreenUI.Hide();
        }
    }

}