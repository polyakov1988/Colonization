using UnityEngine;

namespace Base.Stand
{
    public class Stand : MonoBehaviour
    {
        private bool _isBusy;

        public bool IsBusy => _isBusy;
        public void SetFree() => _isBusy = false;
        public void SetBusy() => _isBusy = true;
    }
}
