using UnityEngine;

namespace Flag.StateMachine.State
{
    public class MovingState : IState
    {
        private readonly Flag _flag;
        private readonly Camera _camera;
        private Plane _plane;
        private Ray _ray;

        public MovingState(Flag flag)
        {
            _flag = flag;
            _plane = new Plane(Vector3.up, Vector3.zero);
            _camera = Camera.main;
        }

        public void Enter() { }

        public void Exit() { }

        public void Update()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
        
            _plane.Raycast(_ray, out float dist);

            _flag.transform.position = _ray.GetPoint(dist);
        }
    }
}
