using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EndGameState : State
    {
        private WonScreenUI wonScreenUI;
        private LoseScreenUI loseScreenUI;
        private Level level;

        public EndGameState(
            GameStateMachine stateMachine,
            WonScreenUI wonScreenUI,
            LoseScreenUI loseScreenUI,
            Level level) : base(stateMachine)
        {
            this.wonScreenUI = wonScreenUI;
            this.loseScreenUI = loseScreenUI;
            this.level = level;
        }


        public override void Enter()
        {
            if (level.GetResult() == Level.ResultState.Win)
            {
                wonScreenUI.Show();
            }
            else if (level.GetResult() == Level.ResultState.Lose)
            {
                loseScreenUI.Show();
            }
            else
            {
                throw new System.Exception("Incorrect Game result");
            }
        }

        public override void Exit()
        {
            wonScreenUI.Hide();
            loseScreenUI.Hide();
        }
    }

}