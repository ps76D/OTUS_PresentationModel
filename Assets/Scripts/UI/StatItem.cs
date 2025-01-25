using System;
using System.Collections.Generic;
using TMPro;
using UI.Model;
using UniRx;
using UnityEngine;

namespace UI
{
    public sealed class StatItem : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_Text _statName;
        
        private IStatItemModel _viewModel;
        
        private readonly List<IDisposable> _disposables = new();

        public void Initialize(IStatItemModel viewModel)
        {
            _viewModel = viewModel;
            
            _disposables.Add(viewModel.CharacterStat.Value.Subscribe(UpdateStatText));
        }

        private void UpdateStatText(int value)
        {
            SetStatData(_viewModel);
        }

        private void SetStatData(IStatItemModel view)
        {
            _statName.text = view.StatText;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}