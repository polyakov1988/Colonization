using System;
using UnityEngine;

namespace Base.Stand
{
    public class StandHandler : MonoBehaviour
    {
        private Stand[] _stands;

        public void Init()
        {
            _stands = transform.GetComponentsInChildren<Stand>();
        }

        public Vector3 GetStandPositionByIndex(int index)
        {
            if (index >= _stands.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            
            Stand stand = _stands[index];
            stand.SetBusy();
            
            return stand.transform.position;
        }
    }
}
