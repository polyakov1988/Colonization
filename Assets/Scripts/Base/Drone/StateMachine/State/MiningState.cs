using System;
using DG.Tweening;
using UnityEngine;

namespace Base.Drone.StateMachine.State
{
    public class MiningState : IState
    {
        private const float FlightAltitude = 10f;
        private const float MoveYDuration = 3f;
        private const float LookAtDuration = 1f;
        
        private readonly Drone _drone;
        private readonly DroneStateMachine _stateMachine;
        
        public MiningState(Drone drone, DroneStateMachine stateMachine)
        {
            _drone = drone;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _drone.Animator.StartMoving();

            TakeCube();
        }
        
        public void Exit()
        {
            _drone.Animator.StopMoving();
        }

        private void TakeCube()
        {
            Transform transform = _drone.transform;
            Vector3 target = _drone.TargetPosition;
            
            Guid uid = Guid.NewGuid();
            
            Sequence sequence = DOTween.Sequence();
            sequence.id = uid;
        
            sequence.Append(transform.DOMoveY(FlightAltitude, MoveYDuration));
            sequence.Append(transform.DOLookAt(new Vector3(target.x, FlightAltitude, target.z), LookAtDuration));
            sequence.Append(transform.DOMove(new Vector3(target.x, FlightAltitude, target.z), 
                Vector3.Distance(transform.position, target) / _drone.Speed));

            Tween moveY = transform.DOMoveY(0, MoveYDuration);
            
            moveY.OnUpdate(() =>
            {
                if (_drone.HasCube)
                    DOTween.Kill(uid);

            });

            sequence.Append(moveY);
            sequence.OnKill(DeliverCube);
        }

        private void DeliverCube()
        {
            Transform transform = _drone.transform;
            Vector3 basePosition = transform.parent.position;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOMoveY(FlightAltitude, MoveYDuration));
            sequence.Append(transform.DOLookAt(new Vector3(basePosition.x, FlightAltitude, basePosition.z), LookAtDuration));
            sequence.Append(transform.DOMove(new Vector3(basePosition.x, FlightAltitude, basePosition.z), 
                Vector3.Distance(transform.position, basePosition) / _drone.Speed));

            sequence.OnComplete(() =>
            {
                ReleaseCube();
                GoToStand();
            });
        }

        private void ReleaseCube()
        {
            Cube cube = _drone.GetComponentInChildren<Cube>();
            
            cube.transform.SetParent(null);
            cube.OnGravity();
        }
        
        private void GoToStand()
        {
            Transform transform = _drone.transform;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOLookAt(new Vector3(_drone.StandPosition.x, FlightAltitude, _drone.StandPosition.z), LookAtDuration));
            sequence.Append(transform.DOMove(new Vector3(_drone.StandPosition.x, FlightAltitude, _drone.StandPosition.z), 
                Vector3.Distance(transform.position, _drone.StandPosition) / _drone.Speed));
            sequence.Append(transform.DOMoveY(_drone.StandPosition.y, MoveYDuration));

            sequence.OnComplete(SwitchStateToIdling);
        }

        private void SwitchStateToIdling()
        {
            _stateMachine.SwitchState<IdlingState>();
        }
    }
}