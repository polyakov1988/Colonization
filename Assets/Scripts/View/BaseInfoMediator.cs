using System;
using Base;
using UnityEngine;

namespace View
{
    public class BaseInfoMediator : IDisposable
    {
        private readonly BaseSpawner _baseSpawner;
        private readonly BaseInfoView _baseInfoView;

        public BaseInfoMediator(BaseSpawner baseSpawner, BaseInfoView baseInfoView)
        {
            _baseSpawner = baseSpawner;
            _baseInfoView = baseInfoView;
        }

        public void Init()
        {
            _baseSpawner.BaseCreatedForView += OnBaseCreated;
        }

        private void OnBaseCreated(ScoreCounter scoreCounter, Color color)
        {
            _baseInfoView.AddScoreView(scoreCounter, color);
        }

        public void Dispose()
        {
            _baseSpawner.BaseCreatedForView -= OnBaseCreated;
        }
    }
}