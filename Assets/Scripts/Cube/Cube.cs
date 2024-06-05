using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public event Action<Cube> Moved;
    public event Action<Cube> Received;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnGravity()
    {
        _rigidbody.useGravity = true;
    }
    
    public void OffGravity()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void InvokeMoved()
    {
        Moved?.Invoke(this);
    }
    
    public void InvokeReceived()
    {
        Received?.Invoke(this);
    }
}
