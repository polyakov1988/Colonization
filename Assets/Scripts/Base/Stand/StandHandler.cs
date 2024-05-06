using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Stand
{
    public class StandHandler : MonoBehaviour
    {
        private List<Stand> _stands;

        public event Action FreePlacesEnded; 

        public void Init()
        {
            _stands = transform.GetComponentsInChildren<Stand>().ToList();
        }

        public Stand GetStand()
        {
            Stand stand = _stands.Find(stand => stand.IsBusy == false);
            
            stand.SetBusy();
            
            if (HasFreePlaces() == false)
                FreePlacesEnded?.Invoke();
            
            return stand;
        }

        private bool HasFreePlaces()
        {
            return _stands.Find(stand => stand.IsBusy == false);
        }
    }
}
