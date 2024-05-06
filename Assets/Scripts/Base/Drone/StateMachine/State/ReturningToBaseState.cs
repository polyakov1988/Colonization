using DG.Tweening;
using UnityEngine;

namespace Base.Drone.StateMachine.State
{
    public class ReturningToBaseState : IState
    {
        private const float FlightAltitude = 10f;
        private const float MoveYDuration = 3f;
        private const float LookAtDuration = 1f;
        
        private readonly Drone _drone;
        private readonly DroneStateMachine _stateMachine;
        
        public ReturningToBaseState(Drone drone, DroneStateMachine stateMachine)
        {
            _drone = drone;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            ReturnToBase();
        }
        
        public void Exit()
        {
            _drone.Animator.StopMoving();
        }

        private void ReturnToBase()
        {
            Transform transform = _drone.transform;
            Vector3 standPosition = _drone.Stand.transform.position;
                
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOLookAt(new Vector3(standPosition.x, FlightAltitude, standPosition.z), LookAtDuration));
            sequence.Append(transform.DOMove(new Vector3(standPosition.x, FlightAltitude, standPosition.z), 
                Vector3.Distance(transform.position, standPosition) / _drone.Speed));
            sequence.Append(transform.DOMoveY(standPosition.y, MoveYDuration));

            sequence.OnComplete(SwitchStateToIdling);
        }
        
        private void SwitchStateToIdling()
        {
            _stateMachine.SwitchState<IdlingState>();
        }
    }
}