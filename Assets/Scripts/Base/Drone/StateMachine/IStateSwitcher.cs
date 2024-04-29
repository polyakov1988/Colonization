using Base.Drone.StateMachine.State;

namespace Base.Drone.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}
