using Base;
using UnityEngine;
using View;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private BaseSpawner _baseSpawner;
    [SerializeField] private BaseInfoView _baseInfoView;

    private void Awake()
    {
       
        BaseInfoMediator mediator = new (_baseSpawner, _baseInfoView);
        mediator.Init();
        _baseSpawner.Init();
    }
}