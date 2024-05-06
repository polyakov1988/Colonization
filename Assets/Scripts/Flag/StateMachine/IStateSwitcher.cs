using Flag.StateMachine.State;

namespace Flag.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}