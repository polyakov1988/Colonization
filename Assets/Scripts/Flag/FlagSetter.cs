using Base;
using Flag.StateMachine.State;
using UnityEngine;

namespace Flag
{
    public class FlagSetter : MonoBehaviour
    {
        [SerializeField] private FlagSpawner _flagSpawner;
        [SerializeField] private PlaneMouseHandler _planeMouseHandler;
        
        private Flag _flag;
        private BaseFlagHandler _baseFlagHandler;

        private void OnEnable()
        {
            _planeMouseHandler.MouseDown += OnPlaneMouseDown;
        }

        private void OnDisable()
        {
            _planeMouseHandler.MouseDown -= OnPlaneMouseDown;
            _flag.FlagInstalled -= OnFlagInstalled;
        }

        private void Awake()
        {
            _flag = _flagSpawner.Get();
            _flag.FlagInstalled += OnFlagInstalled;
            _flag.gameObject.SetActive(false);
            
        }
        
        private void SwitchActivation()
        {
            _flag.gameObject.SetActive(!_flag.gameObject.activeSelf);
        }

        public void SubscribeToUsingFlag(BaseFlagHandler baseFlagHandler)
        {
            if (baseFlagHandler != null)
                baseFlagHandler.FlagUsed -= SwitchActivation;

            _baseFlagHandler = baseFlagHandler;
            _baseFlagHandler.FlagUsed += SwitchActivation;
        }

        private void OnPlaneMouseDown()
        {
            if (_flag.gameObject.activeSelf)
            {
                _flag.FlagStateMachine.SwitchState<StandingState>();
            }
        }

        private void OnFlagInstalled(Transform position)
        {
            _baseFlagHandler.InvokeFlagInstalled(position);
        }
    }
}