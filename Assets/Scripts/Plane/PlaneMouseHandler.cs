using System;
using UnityEngine;

namespace Base
{
    public class PlaneMouseHandler : MonoBehaviour
    {
        public event Action MouseDown;

        private void OnMouseDown()
        {
            MouseDown?.Invoke();
        }
    }
}