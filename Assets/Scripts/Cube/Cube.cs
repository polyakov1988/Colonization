using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public event Action<Cube> Used;
    
    public bool IsReserved { get; private set; }
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init()
    {
        IsReserved = false;
    }

    public void Reserve()
    {
        IsReserved = true;
    }

    public void OnGravity()
    {
        _rigidbody.useGravity = true;
    }
    
    public void OffGravity()
    {
        _rigidbody.useGravity = false;
    }

    public void InvokeUsed()
    {
        Used?.Invoke(this);
    }
}
