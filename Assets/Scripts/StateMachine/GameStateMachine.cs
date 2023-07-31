using System.Collections.Generic;
using System;

namespace Platformer
{
    public class GameStateMachine
    {
        private Dictionary<Type, State> stateDictionary;
        private State currentState;

        public GameStateMachine()
        {
        }

        public void AddStates(Dictionary<Type, State> stateDictionary)
        {
            this.stateDictionary = stateDictionary;
        }

        public void EnterIn<T>() where T : State
        {
            currentState?.Exit();

            if (stateDictionary.TryGetValue(typeof(T), out State newState))
            {
                currentState = newState;
                newState.Enter();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
