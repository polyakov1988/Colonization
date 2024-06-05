using System;
using UnityEngine;

namespace Base
{
    public class BaseFlagHandler : MonoBehaviour
    {
        [SerializeField] private BaseMouseHandler _baseMouseHandler;
        
        private bool _hasFlag;

        public event Action FlagUsed;
        public event Action<Transform> FlagInstalled;
        
        private void OnEnable()
        {
            _baseMouseHandler.MouseDown += InvokeFlagUsed;
        }

        private void OnDisable()
        {
            _baseMouseHandler.MouseDown -= InvokeFlagUsed;
        }
        
        public void Awake()
        {
            _hasFlag = true;
        }
        
        private void InvokeFlagUsed()
        {
            if (_hasFlag)
            {
                FlagUsed?.Invoke();
            }
        }

        public void InvokeFlagInstalled(Transform position)
        {
            if (_hasFlag)
            {
                FlagInstalled?.Invoke(position);
            }
        }

        public void TakeFlag()
        {
            _hasFlag = false;
        }
    }
}