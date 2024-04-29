using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const int MovementCameraButton = 1;
    
    public event Action<Vector3> RightButtonClicked;
    public event Action<float> MouseScrolled;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(MovementCameraButton))
        {
            RightButtonClicked?.Invoke(Input.mousePosition);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            MouseScrolled?.Invoke(Input.mouseScrollDelta.y);
        }
    }
}
