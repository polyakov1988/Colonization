using UnityEngine;

namespace Flag.StateMachine.State
{
    public class StandingState : IState
    {
        private readonly Flag _flag;
        private readonly Camera _camera;
        private Plane _plane;
        private Ray _ray;

        public StandingState(Flag flag)
        {
            _flag = flag;
            _plane = new Plane(Vector3.up, Vector3.zero);
            _camera = Camera.main;
        }

        public void Enter()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            _plane.Raycast(_ray, out float dist);
            
            Vector3 position = _ray.GetPoint(dist);

            _flag.transform.position = position;
            _flag.InvokeFlagInstalled(_flag.transform);
        }

        public void Exit() { }

        public void Update() { }
    }
}