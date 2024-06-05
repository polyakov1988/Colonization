using UnityEngine;

namespace Base
{
    public class BaseRenderer : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private BaseMouseHandler _mouseHandler;
        [SerializeField] private Outline _outline;
        [SerializeField] private float _outlineWidth;
        
        private void OnEnable()
        {
            _mouseHandler.MouseOver += OnMouseOver;
            _mouseHandler.MouseExit += OnMouseExit;
        }

        private void OnDisable()
        {
            _mouseHandler.MouseOver -= OnMouseOver;
            _mouseHandler.MouseExit -= OnMouseExit;
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }

        private void OnMouseOver()
        {
            _outline.OutlineWidth = _outlineWidth;
        }
        
        private void OnMouseExit()
        {
            _outline.OutlineWidth = 0;
        }
    }
}