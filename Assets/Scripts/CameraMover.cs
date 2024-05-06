using System;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;
    [SerializeField] private float _positionXMin;
    [SerializeField] private float _positionXMax;
    [SerializeField] private float _positionZMin;
    [SerializeField] private float _positionZMax;
    
    [SerializeField] private float _speed;
    [SerializeField] private InputHandler _inputHandler;

    private float _minDistance;

    private float _camSize;
    private float _zoomDelta;
    private Plane _plane;

    private Coroutine _coroutine;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        _minDistance = 0.1f;
    }

    private void OnEnable()
    {
        _inputHandler.RightButtonClicked += Move;
        _inputHandler.MouseScrolled += Zoom;
    }

    private void OnDisable()
    {
        _inputHandler.RightButtonClicked -= Move;
        _inputHandler.MouseScrolled -= Zoom;
    }

    private void Move(Vector3 mousePosition)
    {
        if (_coroutine != null) 
            StopCoroutine(_coroutine);

        Ray ray = _camera.ScreenPointToRay(mousePosition);
        
        _plane.Raycast(ray, out float dist);
        
        _coroutine = StartCoroutine(MoveToCursor(ray.GetPoint(dist)));
    }

    private IEnumerator MoveToCursor(Vector3 mousePosition)
    {
        Vector3 clampedPosition = new(Math.Clamp(mousePosition.x, _positionXMin, _positionXMax), 
            mousePosition.y, 
            Math.Clamp(mousePosition.z, _positionZMin, _positionZMax));
        
        while (Vector3.Distance(transform.position, clampedPosition) >= _minDistance)
        {
            transform.position = Vector3.Lerp(transform.position, clampedPosition, _speed * Time.deltaTime);

            yield return null;
        }
    }
    
    private void Zoom(float value)
    {
        _zoomDelta = _camSize - value;
        _camSize = Mathf.Clamp(_zoomDelta, _zoomMin, _zoomMax);
        _camera.orthographicSize = _camSize;
    }
}