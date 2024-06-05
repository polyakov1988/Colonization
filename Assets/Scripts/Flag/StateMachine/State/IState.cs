namespace Flag.StateMachine.State
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}