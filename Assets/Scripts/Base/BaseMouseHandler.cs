using System;
using UnityEngine;

namespace Base
{
    public class BaseMouseHandler : MonoBehaviour
    {
        public event Action MouseOver;
        public event Action MouseExit;
        public event Action MouseDown;
        
        private void OnMouseOver()
        {
            MouseOver?.Invoke();
        }
        
        private void OnMouseExit()
        {
            MouseExit?.Invoke();
        }

        private void OnMouseDown()
        {
            MouseDown?.Invoke();
        }
    }
}