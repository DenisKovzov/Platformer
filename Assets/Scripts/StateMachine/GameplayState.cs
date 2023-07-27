using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class GameplayState : State
    {
        private IPlayerInput input;
        public GameplayState(GameStateMachine stateMachine, IPlayerInput input) : base(stateMachine)
        {
            this.input = input;
        }

        public override void Enter()
        {
            input.OnTogglePause += IPlayerInput_OnTogglePause;
        }

        // Test
        private float elapsedTime;
        private float timeToWin = 10f;
        public override void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeToWin)
            {
                StateMachine.EnterIn<EndGameState>();
            }

        }

        public override void Exit()
        {
            input.OnTogglePause -= IPlayerInput_OnTogglePause;
        }


        private void IPlayerInput_OnTogglePause()
        {

            StateMachine.EnterIn<PauseState>();
        }
    }

}