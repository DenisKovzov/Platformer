namespace Platformer
{
    public abstract class State
    {
        protected GameStateMachine StateMachine;

        protected State(GameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {

        }
    }

}