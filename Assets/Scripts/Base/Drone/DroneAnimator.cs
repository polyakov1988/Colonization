using UnityEngine;

namespace Base.Drone
{
    [RequireComponent(typeof(Animator))]
    public class DroneAnimator : MonoBehaviour
    {
        private const string IsMoving = "IsMoving";

        private Animator _animator;

        public void Init()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void StartMoving()
        {
            _animator.SetBool(IsMoving, true);
        }
        
        public void StopMoving()
        {
            _animator.SetBool(IsMoving, false);
        }
    }
}