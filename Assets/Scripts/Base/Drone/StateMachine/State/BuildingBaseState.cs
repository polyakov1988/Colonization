using System;
using DG.Tweening;
using UnityEngine;

namespace Base.Drone.StateMachine.State
{
    public class BuildingBaseState : IState
    {
        private const float FlightAltitude = 10f;
        private const float MoveYDuration = 3f;
        private const float LookAtDuration = 1f;
        
        private readonly Drone _drone;
        private readonly DroneStateMachine _stateMachine;
        
        public BuildingBaseState(Drone drone, DroneStateMachine stateMachine)
        {
            _drone = drone;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _drone.SetBusy();
            _drone.Animator.StartMoving();

            MoveToFlag();
        }
        
        public void Exit()
        {
            _drone.Animator.StopMoving();
        }

        private void MoveToFlag()
        {
            Transform transform = _drone.transform;
            Vector3 target = _drone.FlagPosition.position;
            
            Sequence sequence = DOTween.Sequence();
        
            sequence.Append(transform.DOMoveY(FlightAltitude, MoveYDuration));
            sequence.Append(transform.DOLookAt(new Vector3(target.x, FlightAltitude, target.z), LookAtDuration));
            sequence.Append(transform.DOMove(new Vector3(target.x, FlightAltitude, target.z), 
                Vector3.Distance(transform.position, target) / _drone.Speed));

            sequence.OnComplete(() =>
            {
                _drone.InvokeReadyToBuildNewBase();
            });
        }
    }
}