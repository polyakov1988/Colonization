using System;
using Flag.StateMachine;
using Flag.StateMachine.State;
using UnityEngine;

namespace Flag
{
    public class Flag : MonoBehaviour
    {
        private FlagStateMachine _stateMachine;

        public FlagStateMachine FlagStateMachine => _stateMachine;

        public event Action<Transform> FlagInstalled;

        private void OnEnable()
        {
            if (_stateMachine != null) 
                _stateMachine.SwitchState<MovingState>();
        }
        
        private void OnDisable()
        {
            _stateMachine.CleatState();
        }

        private void Awake()
        {
            _stateMachine = new FlagStateMachine(this);
        }
        
        private void Update()
        {
            _stateMachine.Update();
        }

        public void InvokeFlagInstalled(Transform position)
        {
            FlagInstalled?.Invoke(position);
        }
    }
}