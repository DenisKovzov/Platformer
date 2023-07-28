using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class StartGameState : State
    {
        private Vector2 startPoint;
        private Player player;
        private List<IResetable> resetableObjectList;

        public StartGameState(GameStateMachine stateMachine, Vector2 startPoint, Player player, List<IResetable> resetableObjectList) : base(stateMachine)
        {
            this.startPoint = startPoint;
            this.player = player;
            this.resetableObjectList = resetableObjectList;
        }

        public override void Enter()
        {
            player.Reset();
            player.transform.position = startPoint;

            foreach (var resetableObject in resetableObjectList)
            {
                resetableObject.Reset();
            }

            StateMachine.EnterIn<GameplayState>();
        }
    }
}
