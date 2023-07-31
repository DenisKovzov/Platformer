using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class GameplayState : State
    {
        private IPlayerInput input;
        private Level level;
        public GameplayState(GameStateMachine stateMachine, IPlayerInput input, Level level) : base(stateMachine)
        {
            this.input = input;
            this.level = level;
        }

        public override void Enter()
        {
            input.OnTogglePause += IPlayerInput_OnTogglePause;
            level.OnGameResult += Level_OnGameResult;
        }

        public override void Exit()
        {
            input.OnTogglePause -= IPlayerInput_OnTogglePause;
            level.OnGameResult -= Level_OnGameResult;
        }

        private void Level_OnGameResult()
        {
            StateMachine.EnterIn<EndGameState>();
        }

        private void IPlayerInput_OnTogglePause()
        {

            StateMachine.EnterIn<PauseState>();
        }
    }

}