using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action<Cube> Used;
    
    public bool IsReserved { get; private set; }
    public bool CanBeTaken { get; private set; }

    private void Awake()
    {
        IsReserved = true;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        IsReserved = false;
        CanBeTaken = true;
    }

    public void Reserve()
    {
        IsReserved = true;
    }
    
    public void CancelReserve()
    {
        IsReserved = false;
    }

    public void Take()
    {
        CanBeTaken = false;
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

    public void InvokeUsed()
    {
        Used?.Invoke(this);
    }
}
